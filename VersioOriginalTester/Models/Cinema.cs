using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VersioOriginalTester.Models
{
    public class Cinema
    {
        public Cinema(){}

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string telephone;
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        private string info;
        public string Info
        {
            get { return info; }
            set { info = value; }
        }        
    }
}
