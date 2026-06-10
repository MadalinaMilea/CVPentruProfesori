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
        public DbSet<SectiuneCustoma> SectiuniCustome { get; set; }
        public DbSet<CampCustom> CampuriCustome { get; set; }
        public DbSet<InregistrareCustoma> InregistrariCustome { get; set; }
        public DbSet<ValoareCamp> ValoriCampuri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // All custom-section FKs use Restrict to avoid cascade cycles.
            // Controllers handle deletions explicitly.
            modelBuilder.Entity<CampCustom>()
                .HasOne<SectiuneCustoma>()
                .WithMany(s => s.Campuri)
                .HasForeignKey(c => c.SectiuneCustomaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InregistrareCustoma>()
                .HasOne<SectiuneCustoma>()
                .WithMany()
                .HasForeignKey(i => i.SectiuneCustomaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InregistrareCustoma>()
                .HasOne<Profesor>()
                .WithMany()
                .HasForeignKey(i => i.ProfesorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ValoareCamp>()
                .HasOne<InregistrareCustoma>()
                .WithMany(i => i.Valori)
                .HasForeignKey(v => v.InregistrareCustomaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ValoareCamp>()
                .HasOne<CampCustom>()
                .WithMany()
                .HasForeignKey(v => v.CampCustomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
