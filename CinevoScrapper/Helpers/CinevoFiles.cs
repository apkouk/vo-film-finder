using System;
using System.IO;
using System.Linq;
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

            Save(path, fileName, extension, content);
        }

        private static void Save(string path, string fileName, string extension, string content)
        {
            var file = path + "\\" + System.IO.Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty)) + "_" + DateTime.Now.Ticks + "." + extension;
            File.WriteAllText(file, content);
            Console.WriteLine(fileName + "." + extension + " SAVED");
        }



        public static void DeleteAllFiles(string path)
        {
            Console.WriteLine("CINEVO FILES: " + Directory.GetFiles(path)[0] + " DELETED");
            File.Delete(Directory.GetFiles(path)[0]);

        }

        internal void MakeComparision()
        {
            if (Directory.Exists(OldFilePath))
            {
                if (Scrapper.JsonContent == null)
                {
                    Scrapper.GetHtmlFromUrl();
                    Scrapper.ScrapeHtml(Path);
                }

                switch (Type)
                {
                    case CinevoEnums.PageTypes.Town:
                        IScrapperTown townComparer = new TownScrapper();
                        townComparer.ScrapeHtml(OldFilePath);
                        HasChanged = !Scrapper.JsonContent.Equals(townComparer.JsonContent);
                        //Scrapper.SaveToDbAsync();
                        break;
                    case CinevoEnums.PageTypes.Cinema:
                        IScrapperCinema cinemaComparer = new IndexScrapper();
                        cinemaComparer.ScrapeHtml(OldFilePath);
                        HasChanged = !Scrapper.JsonContent.Equals(cinemaComparer.JsonContent);
                        //Scrapper.SaveToDbAsync();
                        break;
                }


                Console.WriteLine("CINEVO FILES: " + Type.ToString() + " comparision done");
            }
        }



        public static string GetLastHtmlPath(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.GetFileSystemInfos().OrderByDescending(x => x.LastWriteTime).FirstOrDefault() != null)
            {
                return di.GetFiles().OrderByDescending(x => x.LastWriteTime).FirstOrDefault()?.FullName;
            }
            return string.Empty;
        }
    }
}