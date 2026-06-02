using System.Text.Json;
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
        public IActionResult ImportPublicatii(int profesorId, [FromQuery] int rows = 20)
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

            var publications = new List<object>();

            foreach (var item in items.EnumerateArray())
            {
                string title = "";
                if (item.TryGetProperty("title", out var t) && t.GetArrayLength() > 0)
                    title = t[0].GetString();

                string publisher = "";
                if (item.TryGetProperty("container-title", out var ct) && ct.GetArrayLength() > 0)
                    publisher = ct[0].GetString();

                string year = "";
                if (item.TryGetProperty("published", out var pub) &&
                    pub.TryGetProperty("date-parts", out var dp) &&
                    dp.GetArrayLength() > 0 && dp[0].GetArrayLength() > 0)
                    year = dp[0][0].GetInt32().ToString();

                string urlOut = "";
                if (item.TryGetProperty("DOI", out var doi))
                    urlOut = "https://doi.org/" + doi.GetString();

                publications.Add(new
                {
                    name = title,
                    publisher = publisher,
                    releaseDate = year,
                    url = urlOut,
                    summary = ""
                });
            }

            return Ok(publications);
        }
    }
}
