using System.Collections.Generic;

namespace CinevoScrapper.Models
{
    public class Film
    {
        public string Name { get; set; }
        public string Durantion { get; set; }
        public string Genre { get; set; }
        public string FirstShown { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Description { get; set; }
        public string FilmUrl { get; set; }
        public List<Time> Times { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public string Version { get; set; }
        public string Tag { get; set; }
        public string Country { get; set; }


        public Film()
        {
            Times = new List<Time>();
        }

    }


    
}