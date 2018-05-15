using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinevoScrapper.Models
{
    public class Time
    {
        private string _day;
        public string Day
        {
            get { return _day; }
            set { _day = value; }
        }

        private List<string> _times;
        public List<string> Times
        {
            get { return _times; }
            set { _times = value; }
        }


        public Time()
        {
            Times = new List<string>();
        }

        
    }
}
