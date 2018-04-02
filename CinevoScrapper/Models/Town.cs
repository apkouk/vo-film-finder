using System.Collections.Generic;

namespace CinevoScrapper.Models
{
    public class Town
    {
        private List<Cinema> _cinemas = new List<Cinema>();
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Tag { get; set; }

        public List<Cinema> Cinemas
        {
            get { return _cinemas; }
            set { _cinemas = value; }
        }
    }
}