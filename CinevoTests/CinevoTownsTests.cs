﻿using CinevoScrapper.Classes;
using CinevoScrapper.Interfaces;
using CinevoScrapper.Scrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CinevoTests
{
    [TestClass]
    public class CinevoTownsTests
    {
        private IScrapperTown EqualObjets()
        {
            IScrapperTown townsPage = new TownScrapper
            {
                Path = Properties.CinevoScrapperTest.Default.TownEqual,
                PathProcessed = Properties.CinevoScrapperTest.Default.TownEqualProcessed,
                Url = "https://cartelera.elperiodico.com/cines/",
                ForceRequest = false
            };
            return townsPage;
        }
        private IScrapperTown DiffObjets()
        {
            IScrapperTown townsPage = new TownScrapper
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

        [TestMethod]
        public void Should_return_return_a_list_of_towns()
        {
            IScrapperTown townsPage = EqualObjets();
            var webScrapper = new WebScrapper(townsPage);
            Assert.IsTrue(townsPage.Towns.Count > 5);
        }

    }
}