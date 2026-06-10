namespace Backend.Models
{
    public class CampCustom
    {
        public int Id { get; set; }
        public int SectiuneCustomaId { get; set; }
        public string Eticheta { get; set; }
        public string Tip { get; set; } // text, textarea, date, number, url
        public bool EsteFull { get; set; }
        public int Ordine { get; set; }
    }
}
