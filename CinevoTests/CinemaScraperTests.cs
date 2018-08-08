using CinevoScraper.Interfaces;
using CinevoScraper.Scrapers;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CinevoTests
{
    [TestClass]
    public class CinemaScraperTests
    {

        private IScraperCinema EqualObjets()
        {
            IScraperCinema cinemasPage = new IndexScraper
            {
                Path = Properties.CinevoScraperTest.Default.EqualFilesDownloaded,
                PathProcessed = Properties.CinevoScraperTest.Default.EqualFilesOld,
                Url = Properties.CinevoScraperTest.Default.UrlTowns,
                ForceRequest = false
            };
            return cinemasPage;
        }
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
        public void Should_return_json_data()
        {
            IScraperCinema cinemasPage = EqualObjets();
            cinemasPage.GetHtmlFromUrl();
            Assert.IsTrue(cinemasPage.JsonContent.Length > 100);
        }

        [TestMethod]
        public void Should_return_return_a_list_of_cinemas()
        {
            IScraperCinema cinemasPage = EqualObjets();
            cinemasPage.GetHtmlFromUrl();
            Assert.IsTrue(cinemasPage.Cinemas.Count > 5);
        }
        //[TestMethod]
        //public void Should_save_all_cinemas_in_db()
        //{
        //    IScraperCinema cinemasPage = DiffObjets();
        //    cinemasPage.GetHtmlFromUrl();
        //    Assert.IsTrue(cinemasPage.SaveToDb());
        //}

    }
}
