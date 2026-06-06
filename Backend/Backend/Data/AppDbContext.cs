using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(120);
        }

        public DbSet<Profesor> Profesori { get; set; }
        public DbSet<Educatie> Educatii { get; set; }
        public DbSet<Experienta> Experiente { get; set; }
        public DbSet<Competenta> Competente { get; set; }
        public DbSet<Limba> Limbi { get; set; }
        public DbSet<Certificare> Certificari { get; set; }
        public DbSet<Publicatie> Publicatii { get; set; }
        public DbSet<Link> Linkuri { get; set; }
        public DbSet<SectiuneCv> Sectiuni { get; set; }
        public DbSet<Premiu> Premii { get; set; }
        public DbSet<Proiect> Proiecte { get; set; }
        public DbSet<Interes> Interese { get; set; }
        public DbSet<Referinta> Referinte { get; set; }
        public DbSet<Voluntariat> Voluntariate { get; set; }
    }
}
