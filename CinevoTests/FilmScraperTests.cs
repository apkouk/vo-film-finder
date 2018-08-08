using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using CinevoScraper.Interfaces;
using CinevoScraper.Models;
using CinevoScraper.Scrapers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CinevoTests
{
    [TestClass]
    public class FilmScraperTests
    {

        private IScraperCinema DiffObjets()
        {
            IScraperCinema cinemasPage = new IndexScraper
            {
                Path = Properties.CinevoScraperTest.Default.DiffFilesDownloaded,
                PathProcessed = Properties.CinevoScraperTest.Default.DiffFilesOld,
                Url = Properties.CinevoScraperTest.Default.UrlTowns,
                ForceRequest = false
            };
            return cinemasPage;
        }

        [TestMethod]
        public void Should_return_a_list_of_films_in_cinema()
        {
            IScraperCinema cinemas = DiffObjets();
            cinemas.GetHtmlFromUrl();

            foreach (Cinema cinema in cinemas.Cinemas)
            {
                IScraperFilms filmScraper = new CinemaScraper
                {
                    Path = Properties.CinevoScraperTest.Default.Films,
                    PathProcessed = Properties.CinevoScraperTest.Default.FilmsOld,
                    Url = cinema.Url,
                    Cinema = cinema,
                    ForceRequest = false
                };
              
                filmScraper.GetHtmlFromUrl();
            }
            Assert.IsTrue(cinemas.Cinemas.Any(x => x.Films.Count > 0));
        }

        [TestMethod]
        public void Should_return_a_list_of_films_with_info()
        {
            IScraperCinema cinemas = DiffObjets();
            cinemas.GetHtmlFromUrl();

            foreach (Cinema cinema in cinemas.Cinemas)
            {
                IScraperFilms filmScraper = new CinemaScraper
                {
                    Path = Properties.CinevoScraperTest.Default.Films,
                    PathProcessed = Properties.CinevoScraperTest.Default.FilmsOld,
                    Url = cinema.Url,
                    Cinema = cinema,
                    ForceRequest = false
                };
                filmScraper.GetHtmlFromUrl();
            }


            foreach (Cinema cinema in  cinemas.Cinemas)
            {
                foreach (Film film in cinema.Films)
                {
                    IScraperFilmInfo filmInfoScraper = new FilmScraper
                    {
                        Path = Properties.CinevoScraperTest.Default.FilmInfo,
                        PathProcessed = Properties.CinevoScraperTest.Default.FilmInfoOld,
                        Film = film,
                        ForceRequest = false
                    };
                    filmInfoScraper.GetHtmlFromUrl();
                }
            }

            List<Cinema> cinemaTest = cinemas.Cinemas.Where(x => x.Films.Any(y => y.Version != "(VE)")).ToList();
            Assert.IsTrue(cinemaTest.Count > 0);
        }
    }
}
