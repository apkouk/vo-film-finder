using System.Collections.Generic;

namespace CinevoScrapper.Models
{
    public class Town
    {
        public List<Cinema> Cinemas { get; set; } = new List<Cinema>();
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Tag { get; set; }
        public decimal Lattitude { get; set; }
        public decimal Longitude { get; set; }
    }
}