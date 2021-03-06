﻿using System.IO;
using CinevoScraper.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CinevoTests
{
    [TestClass]
    public class HelpersTest
    {
        readonly string path = Properties.CinevoScraperTest.Default.Temporal;
        readonly string extension = "html";
        readonly string content = "TESTING CONTENT";

        [TestMethod]
        public void Shoud_save_a_file()
        {
            CinevoFiles.SaveToFile(path, CinevoEnums.PageTypes.Town.ToString(), extension, content);
            Assert.IsTrue(Directory.GetFiles(path)[0].Contains(CinevoEnums.PageTypes.Town.ToString()));
        }

        [TestMethod]
        public void Shoud_delete_a_file()
        {
            CinevoFiles.DeleteAllFiles(path);
            string[] files = Directory.GetFiles(path);
            Assert.IsTrue(files.Length == 0);
            Directory.Delete(path);
        }

        [TestMethod]
        public void Shoudl_return_the_corrent_chunk()
        {
            string html = "<option value=\"872\" data-name=\"abrera\" ><a href=\"https://cartelera.elperiodico.com/cines/abrera/\">Abrera</a></option>";
            Assert.IsTrue(CinevoStrings.GetChunk(html, "value=\"", "data-name", "\"").Equals("872"));
        }


    }
}
