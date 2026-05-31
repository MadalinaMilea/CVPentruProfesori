namespace Backend.Models
{
    public class Link
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public string Retea { get; set; }
        public string Url { get; set; }
        public int Ordine { get; set; }
    }
}
