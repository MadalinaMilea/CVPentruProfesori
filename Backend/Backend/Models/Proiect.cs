namespace Backend.Models
{
    public class Proiect
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public string Nume { get; set; }
        public string DataStart { get; set; }
        public string DataEnd { get; set; }
        public string Url { get; set; }
        public string Descriere { get; set; }
    }
}
