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
        }
    }
}
