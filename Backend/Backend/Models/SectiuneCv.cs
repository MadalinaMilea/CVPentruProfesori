namespace Backend.Models
{
    public class SectiuneCv
    {
        public int Id { get; set; }
        public string Titlu { get; set; }
        public string Slug { get; set; }
        public bool EsteActiva { get; set; }
        public bool EsteObligatorie { get; set; }
        public int Ordine { get; set; }
    }
}
