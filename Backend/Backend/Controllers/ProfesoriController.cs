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
            var inregistrariIds = _context.InregistrariCustome
                .Where(i => i.ProfesorId == id)
                .Select(i => i.Id)
                .ToList();

            _context.ValoriCampuri.RemoveRange(
                _context.ValoriCampuri.Where(v => inregistrariIds.Contains(v.InregistrareCustomaId)));

            _context.InregistrariCustome.RemoveRange(
                _context.InregistrariCustome.Where(i => i.ProfesorId == id));

            var profesor = _context.Profesori.Find(id);
            _context.Profesori.Remove(profesor);
            _context.SaveChanges();
        }

        [HttpGet("{id}/cv")]
        public CvDto GetCv(int id)
        {
            var profesor = _context.Profesori
                .Include(p => p.Educatii)
                .Include(p => p.Experiente)
                .Include(p => p.Competente)
                .Include(p => p.Limbi)
                .Include(p => p.Certificari)
                .Include(p => p.Publicatii)
                .Include(p => p.Linkuri)
                .Include(p => p.Premii)
                .Include(p => p.Proiecte)
                .Include(p => p.Interese)
                .Include(p => p.Referinte)
                .Include(p => p.Voluntariate)
                .AsSplitQuery()
                .FirstOrDefault(p => p.Id == id);

            return CvMapper.ToDto(profesor);
        }

        [HttpPut("{id}/cv")]
        public void SaveCv(int id, CvDto dto)
        {
            var profesor = _context.Profesori
                .Include(p => p.Educatii)
                .Include(p => p.Experiente)
                .Include(p => p.Competente)
                .Include(p => p.Limbi)
                .Include(p => p.Certificari)
                .Include(p => p.Publicatii)
                .Include(p => p.Linkuri)
                .Include(p => p.Premii)
                .Include(p => p.Proiecte)
                .Include(p => p.Interese)
                .Include(p => p.Referinte)
                .Include(p => p.Voluntariate)
                .AsSplitQuery()
                .FirstOrDefault(p => p.Id == id);

            CvMapper.ApplyDto(profesor, dto);
            _context.SaveChanges();
        }

        [HttpGet("login")]
        public Profesor Login([FromQuery] string username)
        {
            return _context.Profesori.FirstOrDefault(p => (p.Prenume + " " + p.Nume) == username);
        }

        [HttpGet("{id}/sectiuniCustome")]
        public List<ValoriSectiuneDto> GetCustomValues(int id)
        {
            var inregistrari = _context.InregistrariCustome
                .Include(i => i.Valori)
                .Where(i => i.ProfesorId == id)
                .ToList();

            return inregistrari
                .GroupBy(i => i.SectiuneCustomaId)
                .Select(g => new ValoriSectiuneDto
                {
                    SectiuneId = g.Key,
                    Inregistrari = g.OrderBy(i => i.Ordine).Select(i => new InregistrareDto
                    {
                        Id = i.Id,
                        Ordine = i.Ordine,
                        Valori = i.Valori.Select(v => new ValoareCampDto
                        {
                            CampId = v.CampCustomId,
                            Valoare = v.Valoare
                        }).ToList()
                    }).ToList()
                })
                .ToList();
        }

        [HttpPut("{id}/sectiuniCustome")]
        public void SaveCustomValues(int id, List<ValoriSectiuneDto> dto)
        {
            var inregistrariIds = _context.InregistrariCustome
                .Where(i => i.ProfesorId == id)
                .Select(i => i.Id)
                .ToList();

            _context.ValoriCampuri.RemoveRange(
                _context.ValoriCampuri.Where(v => inregistrariIds.Contains(v.InregistrareCustomaId)));

            _context.InregistrariCustome.RemoveRange(
                _context.InregistrariCustome.Where(i => i.ProfesorId == id));

            foreach (var sectiune in dto ?? new List<ValoriSectiuneDto>())
            {
                foreach (var inregistrare in sectiune.Inregistrari ?? new List<InregistrareDto>())
                {
                    var inr = new InregistrareCustoma
                    {
                        SectiuneCustomaId = sectiune.SectiuneId,
                        ProfesorId = id,
                        Ordine = inregistrare.Ordine,
                        Valori = (inregistrare.Valori ?? new List<ValoareCampDto>()).Select(v => new ValoareCamp
                        {
                            CampCustomId = v.CampId,
                            Valoare = v.Valoare ?? ""
                        }).ToList()
                    };
                    _context.InregistrariCustome.Add(inr);
                }
            }

            _context.SaveChanges();
        }
    }
}
