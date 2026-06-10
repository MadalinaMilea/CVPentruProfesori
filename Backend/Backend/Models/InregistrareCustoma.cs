namespace Backend.Models
{
    public class InregistrareCustoma
    {
        public int Id { get; set; }
        public int SectiuneCustomaId { get; set; }
        public int ProfesorId { get; set; }
        public int Ordine { get; set; }
        public List<ValoareCamp> Valori { get; set; } = new();
    }
}
