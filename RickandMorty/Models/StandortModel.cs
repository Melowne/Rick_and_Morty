namespace RickandMorty.Models
{
    public class Standorte
    {
        public Standort[] root { get; set; }
        public Countstandorte info { get; set; }
    }
    public class Countstandorte
    {
        public int count { get; set; }
    }
    public class Standort
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string dimension { get; set; }
        public List<string> residents { get; set; }
        public string url { get; set; }
        public DateTime created { get; set; }
    }
}
