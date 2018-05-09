using System;
using System.IO;
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
                cinemasPage.HasChanged();
                
                if (Properties.CinevoScrapper.Default.CleanDirectories)
                    CleanFiles(cinemasPage.Path, cinemasPage.PathProcessed);

                foreach (Cinema cinema in cinemasPage.Cinemas)
                {
                    IScrapperFilms scrapperCinemaFilms = new FilmScrapper
                    {
                        Path = Properties.CinevoScrapper.Default.PathFilmCinema,
                        PathProcessed = Properties.CinevoScrapper.Default.PathFimlCinemaProcessed,
                        ForceRequest = !Properties.CinevoScrapper.Default.IsTestEnvironment
                    };

                    //var webScrapper = new WebScrapper(cinemasPage);
                    //if (Properties.CinevoScrapper.Default.CleanDirectories)
                    //    webScrapper.CleanFiles();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.ReadLine();
        }

        private static void CleanFiles(string path, string pathProcessed)
        {
            var oldFile = Directory.GetFiles(pathProcessed)[0];
            var newFile = Directory.GetFiles(path)[0];

            var fileName = Directory.GetFiles(path)[0].Substring(newFile.LastIndexOf("\\", StringComparison.Ordinal) + 1);
            File.Delete(oldFile);
            File.Move(newFile, pathProcessed + "\\" + fileName);
        }

        private static void CleanFiles()
        {
        
        }
    }
}