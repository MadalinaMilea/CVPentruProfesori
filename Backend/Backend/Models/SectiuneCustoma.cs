namespace Backend.Models
{
    public class SectiuneCustoma
    {
        public int Id { get; set; }
        public string Titlu { get; set; }
        public int Ordine { get; set; }
        public bool EsteActiva { get; set; }
        public bool EsteObligatorie { get; set; }
        public bool EsteRepetabila { get; set; }
        public List<CampCustom> Campuri { get; set; } = new();
    }
}
