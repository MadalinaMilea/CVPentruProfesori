using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Backend.Data;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicatiiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _http;

        public PublicatiiController(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _http = httpClientFactory.CreateClient();
        }

        [HttpGet("import/{profesorId}")]
        public IActionResult ImportPublicatii(int profesorId, [FromQuery] int rows = 100)
        {
            var profesor = _context.Profesori.Find(profesorId);
            if (profesor == null) return NotFound();

            var authorName = profesor.Prenume + " " + profesor.Nume;
            var url = "https://api.crossref.org/works?query.author=" + Uri.EscapeDataString(authorName) + "&rows=" + rows;

            var response = _http.GetAsync(url).Result;
            if (!response.IsSuccessStatusCode)
                return StatusCode(502, "Crossref error.");

            var json = response.Content.ReadAsStringAsync().Result;
            var doc = JsonDocument.Parse(json);
            var items = doc.RootElement.GetProperty("message").GetProperty("items");

            var collected = new List<(int Year, object Pub)>();

            // Crossref's query.author is a fuzzy relevance search, so it also returns
            // works by other people who share the surname (e.g. a different "Maican").
            // Keep only works that actually list an author matching this professor's
            // first and last name. ORCID-based import would remove the ambiguity fully.
            var wantFamily = Normalize(profesor.Nume);
            var wantGiven = Normalize((profesor.Prenume ?? "").Split(' ')[0]);

            foreach (var item in items.EnumerateArray())
            {
                if (!AuthorMatches(item, wantFamily, wantGiven))
                    continue;

                string title = "";
                if (item.TryGetProperty("title", out var t) && t.GetArrayLength() > 0)
                    title = Clean(t[0].GetString());

                string publisher = "";
                if (item.TryGetProperty("container-title", out var ct) && ct.GetArrayLength() > 0)
                    publisher = Clean(ct[0].GetString());

                int yearNum = 0;
                string year = "";
                if (item.TryGetProperty("published", out var pub) &&
                    pub.TryGetProperty("date-parts", out var dp) &&
                    dp.GetArrayLength() > 0 && dp[0].GetArrayLength() > 0)
                {
                    yearNum = dp[0][0].GetInt32();
                    year = yearNum.ToString();
                }

                string urlOut = "";
                if (item.TryGetProperty("DOI", out var doi))
                    urlOut = "https://doi.org/" + doi.GetString();

                collected.Add((yearNum, new
                {
                    name = title,
                    publisher = publisher,
                    releaseDate = year,
                    url = urlOut,
                    summary = ""
                }));
            }

            // Most recent first; entries without a known year go last.
            var publications = collected
                .OrderByDescending(x => x.Year)
                .Select(x => x.Pub)
                .ToList();

            return Ok(publications);
        }

        // Crossref titles can contain markup (e.g. <i>...</i>) and HTML entities
        // (e.g. &amp;). Strip tags, then decode entities so the PDF reads cleanly.
        private static string Clean(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            s = Regex.Replace(s, "<.*?>", "");
            s = WebUtility.HtmlDecode(s);
            return s.Trim();
        }

        // True if any author on the work has a family name containing wantFamily
        // and a given name containing wantGiven (both already normalized).
        private static bool AuthorMatches(JsonElement item, string wantFamily, string wantGiven)
        {
            if (string.IsNullOrEmpty(wantFamily) || string.IsNullOrEmpty(wantGiven))
                return true;
            if (!item.TryGetProperty("author", out var authors) || authors.ValueKind != JsonValueKind.Array)
                return false;

            foreach (var a in authors.EnumerateArray())
            {
                var family = a.TryGetProperty("family", out var f) ? Normalize(f.GetString()) : "";
                var given = a.TryGetProperty("given", out var g) ? Normalize(g.GetString()) : "";
                if (family.Contains(wantFamily) && given.Contains(wantGiven))
                    return true;
            }
            return false;
        }

        // Lowercase and strip diacritics so "Cătălin" matches "Catalin".
        private static string Normalize(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            var formD = s.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var ch in formD)
                if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                    sb.Append(ch);
            return sb.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant().Trim();
        }
    }
}
