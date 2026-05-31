namespace Backend.Models
{
    public class Certificare
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public string Nume { get; set; }
        public string Emitent { get; set; }
        public string Data { get; set; }
        public string Url { get; set; }
        public int Ordine { get; set; }
    }
}
