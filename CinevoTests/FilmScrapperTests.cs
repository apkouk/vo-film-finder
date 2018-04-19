using CinevoScrapper.Classes;
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
            IScrapperCinema cinemasPage = new CinemaScrapper
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
            IScrapperCinema cinemasPage = DiffObjets();
            var webScrapper = new WebScrapper(cinemasPage);

            foreach (Cinema cinema in cinemasPage.Cinemas)
            {
                IScrapperCinemaFilms cinemasPageFilms = new CinemaFilmsScrapper
                {
                    Path = Properties.CinevoScrapperTest.Default.DiffFilesDownloaded,
                    PathProcessed = Properties.CinevoScrapperTest.Default.DiffFilesOld,
                    Url = cinema.Url,
                    ForceRequest = true
                };

                var webScrapperFilmCinema = new WebScrapper(cinemasPage);
                webScrapper.Page.HasChanged();

            }

            Assert.IsFalse(webScrapper.Page.HasChanged());
        }
    }
}
