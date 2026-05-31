namespace Backend.Models
{
    public class Limba
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public string Nume { get; set; }
        public string Fluenta { get; set; }
        public int Ordine { get; set; }
    }
}
