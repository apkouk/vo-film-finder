using System;
using CinevoScrapper.Classes;
using CinevoScrapper.Interfaces;
using CinevoScrapper.Models;
using CinevoScrapper.Scrappers;

namespace CinevoScrapper
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            IScrapperTown townsPage = new TownScrapper
            {
                Path = Properties.CinevoScrapper.Default.TownScrapper,
                PathProcessed = Properties.CinevoScrapper.Default.TownScrapperProcessed,
                Url = "https://cartelera.elperiodico.com/cines/",
                ForceRequest = true
            };

            try
            {
                var webScrapper = new WebScrapper(townsPage);
                if (Properties.CinevoScrapper.Default.CleanDirectories)
                    webScrapper.CleanFiles();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            
            Console.ReadLine();
        }
    }
}