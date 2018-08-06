using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using CinevoScrapper.Interfaces;
using CinevoScrapper.Models;
using CinevoScrapper.Scrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CinevoTests
{
    [TestClass]
    public class FilmScrapperTests
    {

        private IScrapperCinema DiffObjets()
        {
            IScrapperCinema cinemasPage = new IndexScrapper
            {
                Path = Properties.CinevoScrapperTest.Default.DiffFilesDownloaded,
                PathProcessed = Properties.CinevoScrapperTest.Default.DiffFilesOld,
                Url = Properties.CinevoScrapperTest.Default.UrlTowns,
                ForceRequest = false
            };
            return cinemasPage;
        }

        [TestMethod]
        public void Should_return_a_list_of_films_in_cinema()
        {
            IScrapperCinema cinemas = DiffObjets();
            cinemas.GetHtmlFromUrl();

            foreach (Cinema cinema in cinemas.Cinemas)
            {
                IScrapperFilms filmScrapper = new CinemaScrapper
                {
                    Path = Properties.CinevoScrapperTest.Default.Films,
                    PathProcessed = Properties.CinevoScrapperTest.Default.FilmsOld,
                    Url = cinema.Url,
                    Cinema = cinema,
                    ForceRequest = false
                };
              
                filmScrapper.GetHtmlFromUrl();
            }
            Assert.IsTrue(cinemas.Cinemas.Any(x => x.Films.Count > 0));
        }

        [TestMethod]
        public void Should_return_a_list_of_films_with_info()
        {
            IScrapperCinema cinemas = DiffObjets();
            cinemas.GetHtmlFromUrl();

            foreach (Cinema cinema in cinemas.Cinemas)
            {
                IScrapperFilms filmScrapper = new CinemaScrapper
                {
                    Path = Properties.CinevoScrapperTest.Default.Films,
                    PathProcessed = Properties.CinevoScrapperTest.Default.FilmsOld,
                    Url = cinema.Url,
                    Cinema = cinema,
                    ForceRequest = false
                };
                filmScrapper.GetHtmlFromUrl();
            }


            foreach (Cinema cinema in  cinemas.Cinemas)
            {
                foreach (Film film in cinema.Films)
                {
                    IScrapperFilmInfo filmInfoScrapper = new FilmScrapper
                    {
                        Path = Properties.CinevoScrapperTest.Default.FilmInfo,
                        PathProcessed = Properties.CinevoScrapperTest.Default.FilmInfoOld,
                        Film = film,
                        ForceRequest = false
                    };
                    filmInfoScrapper.GetHtmlFromUrl();
                }
            }

            List<Cinema> cinemaTest = cinemas.Cinemas.Where(x => x.Films.Any(y => y.Version != "(VE)")).ToList();
            Assert.IsTrue(cinemaTest.Count > 0);
        }
    }
}
