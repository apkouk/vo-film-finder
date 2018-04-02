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

            IScrapperCinema cinemasPage = new CinemaScrapper
            {
                Path = Properties.CinevoScrapper.Default.PathTownsCinemas,
                PathProcessed = Properties.CinevoScrapper.Default.PathTownsCinemaProcessed,
                Url = "https://cartelera.elperiodico.com/cines/",
                ForceRequest = !Properties.CinevoScrapper.Default.IsTestEnvironment
            };

            try
            {
                var webScrapper = new WebScrapper(cinemasPage);
                if (Properties.CinevoScrapper.Default.CleanDirectories)
                    webScrapper.CleanFiles();

                foreach (Cinema cinema in cinemasPage.Cinemas)
                {
                    
                }
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