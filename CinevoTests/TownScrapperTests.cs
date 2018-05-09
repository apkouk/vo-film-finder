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
            IScrapperTown townsPage = EqualObjets();
            Assert.IsFalse(townsPage.HasChanged());
        }

        [TestMethod]
        public void Should_say_that_files_are_different()
        {
            IScrapperTown townsPage = DiffObjets();
            Assert.IsTrue(townsPage.HasChanged());
        }

        [TestMethod]
        public void Should_return_json_data()
        {
            IScrapperTown townsPage = EqualObjets();
            townsPage.HasChanged();
            Assert.IsTrue(townsPage.JsonContent.Length > 100);
        }

        [TestMethod]
        public void Should_return_return_a_list_of_towns()
        {
            IScrapperTown townsPage = EqualObjets();
            townsPage.HasChanged();
            Assert.IsTrue(townsPage.Towns.Count > 5);
        }

        [TestMethod]
        public void Should_save_all_towns_in_db()
        {
            IScrapperTown townsPage = DiffObjets();
            townsPage.HasChanged();
            Assert.IsTrue(townsPage.SaveToDb());
        }

    }
}
