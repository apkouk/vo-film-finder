using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersioOriginalTester.Classes
{
    public class Venue
    {
        public Venue() { }

        private string id;
        private string name;
        private string tag;
       
        private string address;
        private string telf;
        private decimal lng;
        private decimal lat;

        private string url;

        public string URL
        {
            get { return url; }
            set { url = value; }
        }
        

        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }        

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public decimal Lattitude
        {
            get { return lat; }
            set { lat = value; }
        }

        public decimal Longitude
        {
            get { return lng; }
            set { lng = value; }
        }


        public string Telephon
        {
            get { return telf; }
            set { telf = value; }
        }


        public string Address
        {
            get { return address; }
            set { address = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
