using System;
using VersioOriginalTester.Classes;
using VersioOriginalTester.Interfaces;
using VersioOriginalTester.Scrappers;

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
