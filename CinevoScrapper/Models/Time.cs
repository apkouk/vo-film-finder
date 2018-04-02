using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinevoScrapper.Models
{
    public class Time
    {
        private DateTime From { get; set; }
        private DateTime To { get; set; }

        public Time(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }
}
