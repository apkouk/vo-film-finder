using System;
using System.Threading.Tasks;
using CinevoScrapper.Classes;
using CinevoScrapper.Interfaces;
using CinevoScrapper.Scrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CinevoTests
{
    [TestClass]
    public class TownScrapperTests
    {
        private IScrapperTown EqualObjets()
        {
            IScrapperTown townsPage = new TownScrapper
            {
                Path = Properties.CinevoScrapperTest.Default.EqualFilesDownloaded,
                PathProcessed = Properties.CinevoScrapperTest.Default.EqualFilesOld,
                Url = Properties.CinevoScrapperTest.Default.UrlTowns,
                ForceRequest = false
            };
            return townsPage;
        }
        private IScrapperTown DiffObjets()
        {
            IScrapperTown townsPage = new TownScrapper
            {
                Path = Properties.CinevoScrapperTest.Default.DiffFilesDownloaded,
                PathProcessed = Properties.CinevoScrapperTest.Default.DiffFilesOld,
                Url = Properties.CinevoScrapperTest.Default.UrlTowns,
                ForceRequest = false
            };
            return townsPage;
        }

        [TestMethod]
        public void Should_say_that_files_are_equal()
        {
            IScrapper townsPage = EqualObjets();
            var webScrapper = new WebScrapper(townsPage);
            Assert.IsFalse(webScrapper.Page.HasChanged());
        }

        [TestMethod]
        public void Should_say_that_files_are_different()
        {
            IScrapper townsPage = DiffObjets();
            var webScrapper = new WebScrapper(townsPage);
            Assert.IsTrue(webScrapper.Page.HasChanged());
        }

        [TestMethod]
        public void Should_return_json_data()
        {
            IScrapper townsPage = EqualObjets();
            var webScrapper = new WebScrapper(townsPage);
            Assert.IsTrue(webScrapper.Page.JsonContent.Length > 100);
        }

        [TestMethod]
        public void Should_return_return_a_list_of_towns()
        {
            IScrapperTown townsPage = EqualObjets();
            var webScrapper = new WebScrapper(townsPage);
            Assert.IsTrue(townsPage.Towns.Count > 5);
        }

        [TestMethod]
        public void Should_save_all_towns_in_db()
        {
            IScrapperTown townsPage = DiffObjets();
            var webScrapper = new WebScrapper(townsPage);
            Assert.IsTrue(townsPage.SaveToDb());
        }

    }
}
