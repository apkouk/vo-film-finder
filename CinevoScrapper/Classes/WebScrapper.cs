using CinevoScrapper.Interfaces;
using System;
using System.IO;

namespace CinevoScrapper.Classes
{
    public class WebScrapper
    {
        public WebScrapper(IScrapper page)
        {
            Page = page;
            Console.WriteLine(page.HasChanged() ? "TO EXECUTE COMPARISION WITH DATABASE" : "NOTHING TO DO, GRAB A BEER!");
        }

        public IScrapper Page { get; set; }

        public void CleanFiles()
        {
            var oldFile = Directory.GetFiles(Page.PathProcessed)[0];
            var newFile = Directory.GetFiles(Page.Path)[0];

            var fileName = Directory.GetFiles(Page.Path)[0].Substring(newFile.LastIndexOf("\\", StringComparison.Ordinal) + 1);
            File.Delete(oldFile);
            File.Move(newFile, Page.PathProcessed + "\\" + fileName);
        }
    }
}