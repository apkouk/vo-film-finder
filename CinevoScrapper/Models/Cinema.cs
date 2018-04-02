namespace CinevoScrapper.Models
{
    public class Cinema
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Url { get; set; }
        public string Info { get; set; }
        public decimal Lattitude { get; set; }
        public decimal Longitude { get; set; }
        public string TownId { get; set; }
        public string Town { get; set; }
    }
}