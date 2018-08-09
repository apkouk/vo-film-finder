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
            string dateExecution = "09082018";


            DateTime startTime = DateTime.Now;

            Console.WriteLine("---------------------------------------  INDEX ---------------------------------------");
            IScraperCinema cinemas = new IndexScraper
            {
                Path = Properties.CinevoScraper.Default.CinemasIndex + dateExecution,
                Url = "https://cartelera.elperiodico.com/cines/",
                ForceRequest = !Convert.ToBoolean(Properties.CinevoScraper.Default.ForceRequest)
            };

            try
            {
                cinemas.GetHtmlFromUrl();

                Console.WriteLine("---------------------------------------  CINEMAS ---------------------------------------");
                foreach (Cinema cinema in cinemas.Cinemas)
                {
                    IScraperFilms cinemaScraper = new CinemaScraper
                    {
                        Path = Properties.CinevoScraper.Default.Cinemas + dateExecution,
                        Url = cinema.Url,
                        Cinema = cinema,
                        ForceRequest = !Convert.ToBoolean(Properties.CinevoScraper.Default.ForceRequest)
                    };
                    cinemaScraper.GetHtmlFromUrl();
                }


                Console.WriteLine("---------------------------------------  FILMS ---------------------------------------");
                foreach (Cinema cinema in cinemas.Cinemas)
                {
                    foreach (Film film in cinema.Films)
                    {
                        IScraperFilmInfo filmInfoScraper = new FilmScraper
                        {
                            Path = Properties.CinevoScraper.Default.Film + dateExecution,
                            Cinema = cinema,
                            Film = film,
                            ForceRequest = !Convert.ToBoolean(Properties.CinevoScraper.Default.ForceRequest)
                        };
                        filmInfoScraper.GetHtmlFromUrl();
                    }
                }
                
                cinemas.SaveToDb();
                Console.WriteLine("SCRAPING DONE AND IN DB!!!");

                DateTime finishTime = DateTime.Now;

                TimeSpan totalTime = finishTime - startTime;
                Console.WriteLine("TOTAL TIME -> " + totalTime.Hours + ":" + totalTime.Minutes);
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