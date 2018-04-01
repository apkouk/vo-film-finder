using CinevoScrapper.Classes;
using CinevoScrapper.Interfaces;
using CinevoScrapper.Scrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CinevoTests
{
    [TestClass]
    public class CinevoTownsTests
    {
        private IScrapper EqualObjets()
        {
            IScrapper townsPage = new TownScrapper
            {
                Path = Properties.CinevoScrapperTest.Default.TownEqual,
                PathProcessed = Properties.CinevoScrapperTest.Default.TownEqualProcessed,
                Url = "https://cartelera.elperiodico.com/cines/",
                ForceRequest = false
            };
            return townsPage;
        }
        private IScrapper DiffObjets()
        {
            IScrapper townsPage = new TownScrapper
            {
                Path = Properties.CinevoScrapperTest.Default.TownDiff,
                PathProcessed = Properties.CinevoScrapperTest.Default.TownDiffProcessed,
                Url = "https://cartelera.elperiodico.com/cines/",
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
    }
}
