using System.Collections.Generic;

namespace CinevoScraper.Models
{
    public class Day
    {
        public string DayOfWeek { get; set; }
        public List<string> Times { get; set; }

        public Day()
        {
            Times = new List<string>();
        }

        
    }
}
