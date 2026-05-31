namespace Backend.Models
{
    public class Competenta
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public string Nume { get; set; }
        public string Nivel { get; set; }
        public string Keywords { get; set; }
        public int Ordine { get; set; }
    }
}
