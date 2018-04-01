using System;
using System.IO;
using CinevoScrapper.Interfaces;
using CinevoScrapper.Scrappers;

namespace CinevoScrapper.Helpers
{
    public class CinevoFiles
    {
        public IScrapper Scrapper { get; set; }
        public string OldFilePath { get; set; }
        public string Path { get; set; }
        public bool HasChanged { get; set; }

        public static void SaveToFile(string path, string fileName, string extension, string content)
        {
            //Move to App.config
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var file = path + "\\" + fileName + "_" + DateTime.Now.Ticks + "." + extension;
            File.WriteAllText(file, content);
            Console.WriteLine(extension.ToUpper() + "-- File " + fileName + extension + " SAVED");
        }

        internal void MakeComparision()
        {
            if (Directory.Exists(OldFilePath))
            {
                IScrapper townComparer = new TownScrapper();
                townComparer.GetContentInJson(OldFilePath);

                if (Scrapper.JsonContent == null)
                {
                    Scrapper.GetHtmlFromUrl();
                    Scrapper.GetContentInJson(Path);
                }

                HasChanged = !Scrapper.JsonContent.Equals(townComparer.JsonContent); 
            }

        }
    }
}