namespace Backend.Models
{
    public class Voluntariat
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public string Organizatie { get; set; }
        public string Pozitie { get; set; }
        public string DataStart { get; set; }
        public string DataEnd { get; set; }
        public string Rezumat { get; set; }
    }
}
