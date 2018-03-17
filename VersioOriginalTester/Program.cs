using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersioOriginalTester.Interfaces;
using VersioOriginalTester.Classes;

namespace VersioOriginalTester
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadWebsites(); 
        }

        private static void LoadWebsites()
        {   
            IScrapper page = new VenuePage();
            page.URL = "https://cartelera.elperiodico.com/cines/";
            WebScrapper venueWebScrapper = new WebScrapper(page);

           
        }
    }
}
