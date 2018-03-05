using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersioOriginalTester.Classes;

namespace VersioOriginalTester
{
    public class FilmFinder
    {      
        public FilmFinder(ArrayList websites)
        {
            foreach (Website website in websites)
            {
               new WebScrapper(website);
            }
        }
    }
}
