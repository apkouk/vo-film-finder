using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CinevoScrapper.Interfaces;
using CinevoScrapper.Models;
using CinevoScrapper.Scrappers;

namespace CinevoScrapper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IScrapperCinema cinemas = new IndexScrapper
            {
                Path = Properties.CinevoScrapper.Default.CinemasIndex + DateTime.Now.ToString("ddMMyyyy"),
                Url = "https://cartelera.elperiodico.com/cines/",
                ForceRequest = !Convert.ToBoolean(Properties.CinevoScrapper.Default.ForceRequest)
            };

            try
            {
                cinemas.GetHtmlFromUrl();

                foreach (Cinema cinema in cinemas.Cinemas)
                {
                    IScrapperFilms filmScrapper = new CinemaScrapper
                    {
                        Path = Properties.CinevoScrapper.Default.Cinemas + DateTime.Now.ToString("ddMMyyyy"),
                        Url = cinema.Url,
                        Cinema = cinema,
                        ForceRequest = !Convert.ToBoolean(Properties.CinevoScrapper.Default.ForceRequest)
                    };
                    filmScrapper.GetHtmlFromUrl();
                }


                foreach (Cinema cinema in cinemas.Cinemas)
                {
                    foreach (Film film in cinema.Films)
                    {
                        IScrapperFilmInfo filmInfoScrapper = new FilmScrapper
                        {
                            Path = Properties.CinevoScrapper.Default.Film + DateTime.Now.ToString("ddMMyyyy"),
                            Film = film,
                            ForceRequest = !Convert.ToBoolean(Properties.CinevoScrapper.Default.ForceRequest)
                        };
                        filmInfoScrapper.GetHtmlFromUrl();
                    }
                }

                List<Cinema> cinemaTest = cinemas.Cinemas.Where(x => x.Films.Any(y => y.Version != "(VE)")).ToList();
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