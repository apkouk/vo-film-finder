using CinevoScrapper.Interfaces;
using System;
using System.IO;

namespace CinevoScrapper.Classes
{
    public class WebScrapper
    {
        public IScrapper Page { get; }
        
        public WebScrapper(IScrapperTown page)
        {
            Page = page;
            Console.WriteLine(page.HasChanged() ? "TO EXECUTE COMPARISION WITH DATABASE" : "NOTHING TO DO, GRAB A BEER!");
        }

        public WebScrapper(IScrapperCinema page)
        {
            Page = page;
            Console.WriteLine(page.HasChanged() ? "TO EXECUTE COMPARISION WITH DATABASE" : "NOTHING TO DO, GRAB A BEER!");
        }

        public void CleanFiles()
        {
       
        }
    }
}