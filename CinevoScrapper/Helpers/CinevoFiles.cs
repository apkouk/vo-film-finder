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
        public CinevoEnums.PageTypes Type { get; set; }

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
                if (Scrapper.JsonContent == null)
                {
                    Scrapper.GetHtmlFromUrl();
                    Scrapper.GetContentInJson(Path);
                }

                switch (Type)
                {
                    case CinevoEnums.PageTypes.Town:
                        IScrapperTown townComparer = new TownScrapper();
                        townComparer.GetContentInJson(OldFilePath);
                        HasChanged = !Scrapper.JsonContent.Equals(townComparer.JsonContent);
                        break;
                    case CinevoEnums.PageTypes.Cinema:
                        IScrapperCinema cinemaComparer = new CinemaScrapper();
                        cinemaComparer.GetContentInJson(OldFilePath);
                        HasChanged = !Scrapper.JsonContent.Equals(cinemaComparer.JsonContent);
                        break;
                }
            }
        }
    }
}