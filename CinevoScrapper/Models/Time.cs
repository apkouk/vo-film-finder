using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinevoScrapper.Models
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
