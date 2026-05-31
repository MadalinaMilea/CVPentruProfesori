namespace Backend.Models
{
    public class Educatie
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public string Institutie { get; set; }
        public string Domeniu { get; set; }
        public string TipStudii { get; set; }
        public string DataStart { get; set; }
        public string DataEnd { get; set; }
        public string Scor { get; set; }
        public int Ordine { get; set; }
    }
}
