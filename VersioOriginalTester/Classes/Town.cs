using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersioOriginalTester.Classes
{
    public class Town
    {
        public Town() { }

        private List<Cinema> cinemas = new List<Cinema>();
        public List<Cinema> Cinemas
        {
            get { return cinemas; }
            set { cinemas = value; }
        }
        
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string url;
        public string URL
        {
            get { return url; }
            set { url = value; }
        }

        private string tag;
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        
        private decimal lat;
        public decimal Lattitude
        {
            get { return lat; }
            set { lat = value; }
        }

        private decimal lng;
        public decimal Longitude
        {
            get { return lng; }
            set { lng = value; }
        }
    }
}
