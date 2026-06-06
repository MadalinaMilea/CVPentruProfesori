using Backend.Models;

namespace Backend.Data
{
    public static class CvMapper
    {
        public static CvDto ToDto(Profesor profesor)
        {
            return new CvDto
            {
                Basics = new BasicsDto
                {
                    Name = profesor.Prenume + " " + profesor.Nume,
                    Label = profesor.Label,
                    Email = profesor.Email,
                    Phone = profesor.Telefon,
                    Url = profesor.Url,
                    Summary = profesor.Summary,
                    Profiles = profesor.Linkuri.Select(l => new ProfileDto
                    {
                        Network = l.Retea,
                        Url = l.Url
                    }).ToList()
                },
                Education = profesor.Educatii.Select(e => new EducationDto
                {
                    Institution = e.Institutie,
                    Area = e.Domeniu,
                    StudyType = e.TipStudii,
                    StartDate = e.DataStart,
                    EndDate = e.DataEnd,
                    Score = e.Scor
                }).ToList(),
                Work = profesor.Experiente.Select(e => new WorkDto
                {
                    Name = e.Nume,
                    Position = e.Pozitie,
                    StartDate = e.DataStart,
                    EndDate = e.DataEnd,
                    Summary = e.Descriere
                }).ToList(),
                Skills = profesor.Competente.Select(c => new SkillDto
                {
                    Name = c.Nume,
                    Level = c.Nivel,
                    Keywords = c.Keywords
                }).ToList(),
                Languages = profesor.Limbi.Select(l => new LanguageDto
                {
                    Language = l.Nume,
                    Fluency = l.Fluenta
                }).ToList(),
                Certificates = profesor.Certificari.Select(c => new CertificateDto
                {
                    Name = c.Nume,
                    Issuer = c.Emitent,
                    Date = c.Data,
                    Url = c.Url
                }).ToList(),
                Publications = profesor.Publicatii.Select(p => new PublicationDto
                {
                    Name = p.Nume,
                    Publisher = p.Editor,
                    ReleaseDate = p.DataPublicare,
                    Url = p.Url,
                    Summary = p.Rezumat
                }).ToList(),
                Awards = profesor.Premii.Select(p => new AwardDto
                {
                    Title = p.Titlu,
                    Awarder = p.Acordant,
                    Date = p.Data,
                    Summary = p.Rezumat
                }).ToList(),
                Projects = profesor.Proiecte.Select(p => new ProjectDto
                {
                    Name = p.Nume,
                    StartDate = p.DataStart,
                    EndDate = p.DataEnd,
                    Url = p.Url,
                    Description = p.Descriere
                }).ToList(),
                Interests = profesor.Interese.Select(i => new InterestDto
                {
                    Name = i.Nume
                }).ToList(),
                References = profesor.Referinte.Select(r => new ReferenceDto
                {
                    Name = r.Nume,
                    Reference = r.Referinta_
                }).ToList(),
                Volunteer = profesor.Voluntariate.Select(v => new VolunteerDto
                {
                    Organization = v.Organizatie,
                    Position = v.Pozitie,
                    StartDate = v.DataStart,
                    EndDate = v.DataEnd,
                    Summary = v.Rezumat
                }).ToList()
            };
        }

        public static void ApplyDto(Profesor profesor, CvDto dto)
        {
            profesor.Label = dto.Basics.Label;
            profesor.Email = dto.Basics.Email;
            profesor.Telefon = dto.Basics.Phone;
            profesor.Url = dto.Basics.Url;
            profesor.Summary = dto.Basics.Summary;

            profesor.Linkuri.Clear();
            foreach (var p in dto.Basics.Profiles ?? new List<ProfileDto>())
                profesor.Linkuri.Add(new Link { Retea = p.Network, Url = p.Url });

            profesor.Educatii.Clear();
            foreach (var e in dto.Education ?? new List<EducationDto>())
                profesor.Educatii.Add(new Educatie
                {
                    Institutie = e.Institution,
                    Domeniu = e.Area,
                    TipStudii = e.StudyType,
                    DataStart = e.StartDate,
                    DataEnd = e.EndDate,
                    Scor = e.Score
                });

            profesor.Experiente.Clear();
            foreach (var e in dto.Work ?? new List<WorkDto>())
                profesor.Experiente.Add(new Experienta
                {
                    Nume = e.Name,
                    Pozitie = e.Position,
                    DataStart = e.StartDate,
                    DataEnd = e.EndDate,
                    Descriere = e.Summary
                });

            profesor.Competente.Clear();
            foreach (var c in dto.Skills ?? new List<SkillDto>())
                profesor.Competente.Add(new Competenta { Nume = c.Name, Nivel = c.Level, Keywords = c.Keywords });

            profesor.Limbi.Clear();
            foreach (var l in dto.Languages ?? new List<LanguageDto>())
                profesor.Limbi.Add(new Limba { Nume = l.Language, Fluenta = l.Fluency });

            profesor.Certificari.Clear();
            foreach (var c in dto.Certificates ?? new List<CertificateDto>())
                profesor.Certificari.Add(new Certificare { Nume = c.Name, Emitent = c.Issuer, Data = c.Date, Url = c.Url });

            profesor.Publicatii.Clear();
            foreach (var p in dto.Publications ?? new List<PublicationDto>())
                profesor.Publicatii.Add(new Publicatie
                {
                    Nume = p.Name,
                    Editor = p.Publisher,
                    DataPublicare = p.ReleaseDate,
                    Url = p.Url,
                    Rezumat = p.Summary
                });

            profesor.Premii.Clear();
            foreach (var p in dto.Awards ?? new List<AwardDto>())
                profesor.Premii.Add(new Premiu { Titlu = p.Title, Acordant = p.Awarder, Data = p.Date, Rezumat = p.Summary });

            profesor.Proiecte.Clear();
            foreach (var p in dto.Projects ?? new List<ProjectDto>())
                profesor.Proiecte.Add(new Proiect { Nume = p.Name, DataStart = p.StartDate, DataEnd = p.EndDate, Url = p.Url, Descriere = p.Description });

            profesor.Interese.Clear();
            foreach (var i in dto.Interests ?? new List<InterestDto>())
                profesor.Interese.Add(new Interes { Nume = i.Name });

            profesor.Referinte.Clear();
            foreach (var r in dto.References ?? new List<ReferenceDto>())
                profesor.Referinte.Add(new Referinta { Nume = r.Name, Referinta_ = r.Reference });

            profesor.Voluntariate.Clear();
            foreach (var v in dto.Volunteer ?? new List<VolunteerDto>())
                profesor.Voluntariate.Add(new Voluntariat { Organizatie = v.Organization, Pozitie = v.Position, DataStart = v.StartDate, DataEnd = v.EndDate, Rezumat = v.Summary });
        }
    }
}
