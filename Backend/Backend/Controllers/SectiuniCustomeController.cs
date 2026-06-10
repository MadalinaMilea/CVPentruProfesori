using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectiuniCustomeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SectiuniCustomeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<SectiuneCustomaDto> GetAll()
        {
            return _context.SectiuniCustome
                .Include(s => s.Campuri.OrderBy(c => c.Ordine))
                .OrderBy(s => s.Ordine)
                .Select(s => new SectiuneCustomaDto
                {
                    Id = s.Id,
                    Titlu = s.Titlu,
                    Ordine = s.Ordine,
                    EsteActiva = s.EsteActiva,
                    EsteObligatorie = s.EsteObligatorie,
                    EsteRepetabila = s.EsteRepetabila,
                    Campuri = s.Campuri.Select(c => new CampCustomDto
                    {
                        Id = c.Id,
                        Eticheta = c.Eticheta,
                        Tip = c.Tip,
                        EsteFull = c.EsteFull,
                        Ordine = c.Ordine
                    }).ToList()
                })
                .ToList();
        }

        [HttpGet("active")]
        public List<SectiuneCustomaDto> GetActive()
        {
            return _context.SectiuniCustome
                .Include(s => s.Campuri.OrderBy(c => c.Ordine))
                .Where(s => s.EsteActiva)
                .OrderBy(s => s.Ordine)
                .Select(s => new SectiuneCustomaDto
                {
                    Id = s.Id,
                    Titlu = s.Titlu,
                    Ordine = s.Ordine,
                    EsteActiva = s.EsteActiva,
                    EsteObligatorie = s.EsteObligatorie,
                    EsteRepetabila = s.EsteRepetabila,
                    Campuri = s.Campuri.Select(c => new CampCustomDto
                    {
                        Id = c.Id,
                        Eticheta = c.Eticheta,
                        Tip = c.Tip,
                        EsteFull = c.EsteFull,
                        Ordine = c.Ordine
                    }).ToList()
                })
                .ToList();
        }

        [HttpPost]
        public SectiuneCustomaDto Create(SectiuneCustomaDto dto)
        {
            var maxCustom = _context.SectiuniCustome.Any() ? _context.SectiuniCustome.Max(s => s.Ordine) : 0;
            var maxBuiltin = _context.Sectiuni.Any() ? _context.Sectiuni.Max(s => s.Ordine) : 0;
            var maxOrdine = Math.Max(maxCustom, maxBuiltin);

            var sectiune = new SectiuneCustoma
            {
                Titlu = dto.Titlu,
                Ordine = maxOrdine + 1,
                EsteActiva = dto.EsteActiva,
                EsteObligatorie = dto.EsteObligatorie,
                EsteRepetabila = dto.EsteRepetabila
            };
            _context.SectiuniCustome.Add(sectiune);
            _context.SaveChanges();

            dto.Id = sectiune.Id;
            dto.Ordine = sectiune.Ordine;
            dto.Campuri = new List<CampCustomDto>();
            return dto;
        }

        [HttpPut("{id}")]
        public SectiuneCustomaDto Update(int id, SectiuneCustomaDto dto)
        {
            var sectiune = _context.SectiuniCustome.Find(id);
            sectiune.Titlu = dto.Titlu;
            sectiune.EsteActiva = dto.EsteActiva;
            sectiune.EsteObligatorie = dto.EsteObligatorie;
            sectiune.EsteRepetabila = dto.EsteRepetabila;
            sectiune.Ordine = dto.Ordine;
            _context.SaveChanges();
            dto.Id = id;
            return dto;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // Manual cascade: delete ValoriCampuri → InregistrariCustome → CampuriCustome → SectiuneCustoma
            var campuriIds = _context.CampuriCustome
                .Where(c => c.SectiuneCustomaId == id)
                .Select(c => c.Id)
                .ToList();

            var inregistrariIds = _context.InregistrariCustome
                .Where(i => i.SectiuneCustomaId == id)
                .Select(i => i.Id)
                .ToList();

            _context.ValoriCampuri.RemoveRange(
                _context.ValoriCampuri.Where(v =>
                    inregistrariIds.Contains(v.InregistrareCustomaId) ||
                    campuriIds.Contains(v.CampCustomId)));

            _context.InregistrariCustome.RemoveRange(
                _context.InregistrariCustome.Where(i => i.SectiuneCustomaId == id));

            _context.CampuriCustome.RemoveRange(
                _context.CampuriCustome.Where(c => c.SectiuneCustomaId == id));

            var sectiune = _context.SectiuniCustome.Find(id);
            _context.SectiuniCustome.Remove(sectiune);
            _context.SaveChanges();
        }

        [HttpPost("{id}/campuri")]
        public CampCustomDto AddCamp(int id, CampCustomDto dto)
        {
            var maxOrdine = _context.CampuriCustome.Where(c => c.SectiuneCustomaId == id).Any()
                ? _context.CampuriCustome.Where(c => c.SectiuneCustomaId == id).Max(c => c.Ordine)
                : 0;

            var camp = new CampCustom
            {
                SectiuneCustomaId = id,
                Eticheta = dto.Eticheta,
                Tip = dto.Tip,
                EsteFull = dto.EsteFull,
                Ordine = maxOrdine + 1
            };
            _context.CampuriCustome.Add(camp);
            _context.SaveChanges();

            dto.Id = camp.Id;
            dto.Ordine = camp.Ordine;
            return dto;
        }

        [HttpPut("{id}/campuri/{campId}")]
        public CampCustomDto UpdateCamp(int id, int campId, CampCustomDto dto)
        {
            var camp = _context.CampuriCustome.Find(campId);
            camp.Eticheta = dto.Eticheta;
            camp.Tip = dto.Tip;
            camp.EsteFull = dto.EsteFull;
            camp.Ordine = dto.Ordine;
            _context.SaveChanges();
            dto.Id = campId;
            return dto;
        }

        [HttpDelete("{id}/campuri/{campId}")]
        public void DeleteCamp(int id, int campId)
        {
            _context.ValoriCampuri.RemoveRange(
                _context.ValoriCampuri.Where(v => v.CampCustomId == campId));

            var camp = _context.CampuriCustome.Find(campId);
            _context.CampuriCustome.Remove(camp);
            _context.SaveChanges();
        }
    }
}
