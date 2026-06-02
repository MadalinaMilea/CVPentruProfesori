using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectiuniController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SectiuniController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<SectiuneCv> GetAll()
        {
            return _context.Sectiuni.ToList();
        }

        [HttpGet("active")]
        public List<SectiuneCv> GetActive()
        {
            return _context.Sectiuni
                .Where(s => s.EsteActiva)
                .OrderBy(s => s.Ordine)
                .ToList();
        }

        [HttpPut("{id}")]
        public SectiuneCv Update(int id, SectiuneCv sectiune)
        {
            var existing = _context.Sectiuni.Find(id);
            existing.EsteActiva = sectiune.EsteActiva;
            existing.EsteObligatorie = sectiune.EsteObligatorie;
            existing.Ordine = sectiune.Ordine;
            _context.SaveChanges();
            return existing;
        }
    }
}