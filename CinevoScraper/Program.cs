using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using CinevoScraper.Interfaces;
using CinevoScraper.Models;
using CinevoScraper.Scrapers;
using CinevoScraper.Helpers;
using CinevoScraper.Helpers;

namespace CinevoScraper
{
    internal class Program
    {
        private static void Main(string[] args)
        {


            //string dateExecution = DateTime.Now.ToString("ddMMyyyy");
            string dateExecution = "09082018";


            DateTime startTime = DateTime.Now;

            try
            {
                Console.WriteLine("---------------------------------------  INDEX ---------------------------------------");

                IScraperCinema cinemas = new IndexScraper
                {
                    Path = CinevoParameters.CinevoSettings.PathCinemasIndex + dateExecution,
                    ForceRequest = CinevoParameters.CinevoSettings.ForceRequest,
                    Url = "https://cartelera.elperiodico.com/cines/"
                };
                cinemas.GetHtmlFromUrl();
                
                Console.WriteLine("---------------------------------------  CINEMAS ---------------------------------------");

                foreach (Cinema cinema in cinemas.Cinemas)
                {
                    IScraperFilms cinemaScraper = new CinemaScraper
                    {
                        Path = CinevoParameters.CinevoSettings.PathCinemas + dateExecution,
                        ForceRequest = CinevoParameters.CinevoSettings.ForceRequest,
                        Url = cinema.Url,
                        Cinema = cinema
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
                            Path = CinevoParameters.CinevoSettings.PathFilms + dateExecution,
                            ForceRequest = CinevoParameters.CinevoSettings.ForceRequest,
                            Cinema = cinema,
                            Film = film
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