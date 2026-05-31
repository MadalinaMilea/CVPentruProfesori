namespace Backend.Models
{
    public class Profesor
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string Label { get; set; }
        public string Telefon { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
        public List<Educatie> Educatii { get; set; } = new List<Educatie>();
        public List<Experienta> Experiente { get; set; } = new List<Experienta>();
        public List<Competenta> Competente { get; set; } = new List<Competenta>();
        public List<Limba> Limbi { get; set; } = new List<Limba>();
        public List<Certificare> Certificari { get; set; } = new List<Certificare>();
        public List<Publicatie> Publicatii { get; set; } = new List<Publicatie>();
        public List<Link> Linkuri { get; set; } = new List<Link>();
    }
}
