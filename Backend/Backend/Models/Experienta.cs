namespace Backend.Models
{
    public class Experienta
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public string Nume { get; set; }
        public string Pozitie { get; set; }
        public string DataStart { get; set; }
        public string DataEnd { get; set; }
        public string Descriere { get; set; }
        public int Ordine { get; set; }
    }
}
