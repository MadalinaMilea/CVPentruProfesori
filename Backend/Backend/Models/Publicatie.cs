namespace Backend.Models
{
    public class Publicatie
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public string Nume { get; set; }
        public string Editor { get; set; }
        public string DataPublicare { get; set; }
        public string Url { get; set; }
        public string Rezumat { get; set; }
        public int Ordine { get; set; }
    }
}
