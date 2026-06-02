using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173").AllowAnyHeader().AllowAnyMethod();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Sectiuni.Any())
    {
        db.Sectiuni.AddRange(
            new SectiuneCv { Titlu = "Work Experience", Slug = "work", EsteActiva = true, EsteObligatorie = false, Ordine = 1 },
            new SectiuneCv { Titlu = "Education", Slug = "education", EsteActiva = true, EsteObligatorie = true, Ordine = 2 },
            new SectiuneCv { Titlu = "Skills", Slug = "skills", EsteActiva = true, EsteObligatorie = false, Ordine = 3 },
            new SectiuneCv { Titlu = "Languages", Slug = "languages", EsteActiva = true, EsteObligatorie = false, Ordine = 4 },
            new SectiuneCv
            {
                Titlu = "Publications",
                Slug = "publications",
                EsteActiva = true,
                EsteObligatorie = false,
                Ordine = 5
            },
            new SectiuneCv
            {
                Titlu = "Certificates",
                Slug = "certificates",
                EsteActiva = false,
                EsteObligatorie = false,
                Ordine = 6
            },
            new SectiuneCv { Titlu = "Projects", Slug = "projects", EsteActiva = false, EsteObligatorie = false, Ordine = 7 },
            new SectiuneCv { Titlu = "Awards", Slug = "awards", EsteActiva = false, EsteObligatorie = false, Ordine = 8 },
            new SectiuneCv { Titlu = "Interests", Slug = "interests", EsteActiva = false, EsteObligatorie = false, Ordine = 9 },
            new SectiuneCv { Titlu = "References", Slug = "references", EsteActiva = false, EsteObligatorie = false, Ordine = 10 },
            new SectiuneCv { Titlu = "Volunteer", Slug = "volunteer", EsteActiva = false, EsteObligatorie = false, Ordine = 11 }
        );
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
