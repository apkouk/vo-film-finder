using CinevoScrapper.Interfaces;
using CinevoScrapper.Scrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CinevoTests
{
    [TestClass]
    public class CinemaScrapperTests
    {

        private IScrapperCinema EqualObjets()
        {
            IScrapperCinema cinemasPage = new CinemaScrapper
            {
                Path = Properties.CinevoScrapperTest.Default.EqualFilesDownloaded,
                PathProcessed = Properties.CinevoScrapperTest.Default.EqualFilesOld,
                Url = Properties.CinevoScrapperTest.Default.UrlTowns,
                ForceRequest = false
            };
            return cinemasPage;
        }
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
        public void Should_say_that_files_are_equal()
        {
            IScrapperCinema cinemasPage = EqualObjets();
            Assert.IsFalse(cinemasPage.HasChanged());
        }

        [TestMethod]
        public void Should_say_that_files_are_different()
        {
            IScrapperCinema cinemasPage = DiffObjets();
            Assert.IsTrue(cinemasPage.HasChanged());
        }

        [TestMethod]
        public void Should_return_json_data()
        {
            IScrapperCinema cinemasPage = EqualObjets();
            cinemasPage.HasChanged();
            Assert.IsTrue(cinemasPage.JsonContent.Length > 100);
        }

        [TestMethod]
        public void Should_return_return_a_list_of_cinemas()
        {
            IScrapperCinema cinemasPage = EqualObjets();
            cinemasPage.HasChanged();
            Assert.IsTrue(cinemasPage.Cinemas.Count > 5);
        }
        [TestMethod]
        public void Should_save_all_cinemas_in_db()
        {
            IScrapperCinema cinemasPage = DiffObjets();
            cinemasPage.HasChanged();
            Assert.IsTrue(cinemasPage.SaveToDb());
        }

    }
}
