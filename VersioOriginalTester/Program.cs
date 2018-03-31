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
            IScrapper townsPage = new TownScrapper();
            townsPage.URL = "https://cartelera.elperiodico.com/cines/";
            WebScrapper webScrapper = new WebScrapper(townsPage);
            
            Console.ReadLine();
        }
    }
}
