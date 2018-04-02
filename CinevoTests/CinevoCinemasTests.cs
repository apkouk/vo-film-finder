using System;
using CinevoScrapper.Classes;
using CinevoScrapper.Interfaces;
using CinevoScrapper.Scrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CinevoTests
{
    [TestClass]
    public class CinevoCinemasTests
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
            IScrapper cinemasPage = EqualObjets();
            var webScrapper = new WebScrapper(cinemasPage);
            Assert.IsFalse(webScrapper.Page.HasChanged());
        }

        [TestMethod]
        public void Should_say_that_files_are_different()
        {
            IScrapper cinemasPage = DiffObjets();
            var webScrapper = new WebScrapper(cinemasPage);
            Assert.IsTrue(webScrapper.Page.HasChanged());
        }

        [TestMethod]
        public void Should_return_json_data()
        {
            IScrapper cinemasPage = EqualObjets();
            var webScrapper = new WebScrapper(cinemasPage);
            Assert.IsTrue(webScrapper.Page.JsonContent.Length > 100);
        }

        [TestMethod]
        public void Should_return_return_a_list_of_cinemas()
        {
            IScrapperCinema cinemasPage = EqualObjets();
            var webScrapper = new WebScrapper(cinemasPage);
            Assert.IsTrue(cinemasPage.Cinemas.Count > 5);
        }


    }
}
