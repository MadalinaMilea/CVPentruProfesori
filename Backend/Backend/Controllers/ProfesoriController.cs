using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesoriController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfesoriController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Profesor> GetAll()
        {
            return _context.Profesori.ToList();
        }

        [HttpPost]
        public Profesor Create(Profesor profesor)
        {
            _context.Profesori.Add(profesor);
            _context.SaveChanges();
            return profesor;
        }

        [HttpPut("{id}")]
        public Profesor Update(int id, Profesor profesor)
        {
            var existing = _context.Profesori.Find(id);
            existing.Nume = profesor.Nume;
            existing.Prenume = profesor.Prenume;
            existing.Email = profesor.Email;
            _context.SaveChanges();
            return existing;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var profesor = _context.Profesori.Find(id);
            _context.Profesori.Remove(profesor);
            _context.SaveChanges();
        }
    }
}
