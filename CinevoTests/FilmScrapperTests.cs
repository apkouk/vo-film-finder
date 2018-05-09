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
            IScrapperCinema cinemas = DiffObjets();
            cinemas.HasChanged();

            foreach (Cinema cinema in cinemas.Cinemas)
            {
                IScrapperFilms filmScrapper = new FilmScrapper
                {
                    Path = Properties.CinevoScrapperTest.Default.DiffFilesDownloaded,
                    PathProcessed = Properties.CinevoScrapperTest.Default.DiffFilesOld,
                    Url = cinema.Url,
                    Cinema = cinema,
                    ForceRequest = true
                };
            }
        }
    }
}
