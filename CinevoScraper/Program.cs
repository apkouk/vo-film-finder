using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CinevoScraper.Interfaces;
using CinevoScraper.Models;
using CinevoScraper.Scrapers;

namespace CinevoScraper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //string dateExecution = DateTime.Now.ToString("ddMMyyyy");
            string dateExecution = "06082018";

            IScraperCinema cinemas = new IndexScraper
            {
                Path = Properties.CinevoScraper.Default.CinemasIndex + dateExecution,
                Url = "https://cartelera.elperiodico.com/cines/",
                ForceRequest = !Convert.ToBoolean(Properties.CinevoScraper.Default.ForceRequest)
            };

            try
            {
                cinemas.GetHtmlFromUrl();

                foreach (Cinema cinema in cinemas.Cinemas)
                {
                    IScraperFilms filmScraper = new CinemaScraper
                    {
                        Path = Properties.CinevoScraper.Default.Cinemas + dateExecution,
                        Url = cinema.Url,
                        Cinema = cinema,
                        ForceRequest = !Convert.ToBoolean(Properties.CinevoScraper.Default.ForceRequest)
                    };
                    filmScraper.GetHtmlFromUrl();
                }


                foreach (Cinema cinema in cinemas.Cinemas)
                {
                    foreach (Film film in cinema.Films)
                    {
                        IScraperFilmInfo filmInfoScraper = new FilmScraper
                        {
                            Path = Properties.CinevoScraper.Default.Film + dateExecution,
                            Film = film,
                            ForceRequest = !Convert.ToBoolean(Properties.CinevoScraper.Default.ForceRequest)
                        };
                        filmInfoScraper.GetHtmlFromUrl();
                    }
                }
                
                IEnumerable<Cinema> defCinemaList = GetOnlyOriginalVersion(cinemas.Cinemas).Where(x => x.OriginalVersionFilms.Any());
                cinemas.Cinemas = defCinemaList.ToList();
                cinemas.SaveToDb();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.ReadLine();
        }

        private static List<Cinema> GetOnlyOriginalVersion(List<Cinema> cinemaTest)
        {
            foreach (var cinema in cinemaTest)
            {
                foreach (var film in cinema.Films)
                {
                    switch (film.Version)
                    {
                        case "(VE)":
                            break;

                        case "(VC)":
                            break;

                        default:
                            cinema.OriginalVersionFilms.Add(film);
                            break;

                    }
                }
            }

            return cinemaTest;

        }

        private static void RemoveFilm(Cinema cinema, Film film, List<Cinema> cinemaTest)
        {
            Cinema cinemaFiltered = cinemaTest.Single(x => x.CinemaId == cinema.CinemaId);
            cinemaFiltered.Films.Remove(film);
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