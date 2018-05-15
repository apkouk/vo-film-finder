using System.Collections.Generic;

namespace CinevoScrapper.Models
{
    public class Film
    {
        public string Name { get; set; }
        //public string Durantion { get; set; }
        //public string Genre { get; set; }
        //public string FirstShown { get; set; }
        //public string Director { get; set; }
        //public string Panel { get; set; }
        public string Description { get; set; }
        public List<Time> Times { get; set; }
        public string Image { get; set; }
        public string Version { get; set; }


        public Film()
        {
            Times = new List<Time>();
        }

    }


    
}