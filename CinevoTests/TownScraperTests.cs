using CinevoScraper.Interfaces;
using CinevoScraper.Scrapers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CinevoTests
{
    [TestClass]
    public class TownScraperTests
    {
        private IScraperTown EqualObjets()
        {
            IScraperTown townsPage = new TownScraper
            {
                Path = Properties.CinevoScraperTest.Default.EqualFilesDownloaded,
                PathProcessed = Properties.CinevoScraperTest.Default.EqualFilesOld,
                Url = Properties.CinevoScraperTest.Default.UrlTowns,
                ForceRequest = false
            };
            return townsPage;
        }
        private IScraperTown DiffObjets() 
        {
            IScraperTown townsPage = new TownScraper
            {
                Path = Properties.CinevoScraperTest.Default.DiffFilesDownloaded,
                PathProcessed = Properties.CinevoScraperTest.Default.DiffFilesOld,
                Url = Properties.CinevoScraperTest.Default.UrlTowns,
                ForceRequest = false
            };
            return townsPage;
        }

        [TestMethod]
        public void Should_say_that_files_are_equal()
        {
            IScraperTown townsPage = EqualObjets();
            Assert.IsFalse(townsPage.HasChanged());
        }

        [TestMethod]
        public void Should_say_that_files_are_different()
        {
            IScraperTown townsPage = DiffObjets();
            Assert.IsTrue(townsPage.HasChanged());
        }

        [TestMethod]
        public void Should_return_json_data()
        {
            IScraperTown townsPage = EqualObjets();
            townsPage.HasChanged();
            Assert.IsTrue(townsPage.JsonContent.Length > 100);
        }

        [TestMethod]
        public void Should_return_return_a_list_of_towns()
        {
            IScraperTown townsPage = EqualObjets();
            townsPage.HasChanged();
            Assert.IsTrue(townsPage.Towns.Count > 5);
        }

        [TestMethod]
        public void Should_save_all_towns_in_db()
        {
            IScraperTown townsPage = DiffObjets();
            townsPage.HasChanged();
            Assert.IsTrue(townsPage.SaveToDb());
        }

    }
}
