using System;
using System.IO;
using VersioOriginalTester.Interfaces;
using VersioOriginalTester.Scrappers;

namespace VersioOriginalTester.Helpers
{
    public class CinevoFiles
    {
        private IScrapper iscrapper;
        public IScrapper IScrapper
        {
            get { return iscrapper; }
            set { iscrapper = value; }
        }

        private string oldFilePath;
        public string OldFilePath
        {
            get { return oldFilePath; }
            set { oldFilePath = value; }
        }

        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public CinevoFiles() { }

        public static void SaveToFile(string path, string fileName, string extension, string content)
        {
            //Move to App.config
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string file = path + "\\" + fileName + "_" + DateTime.Now.Ticks + "." + extension;
            System.IO.File.WriteAllText(file, content);
            Console.WriteLine(extension.ToUpper() + "-- File " + fileName + extension + " SAVED");
        }

        internal bool AreFilesDifferent()
        {
            string oldFile = Directory.GetFiles(OldFilePath)[0];
            string newFile = string.Empty;

            if (Directory.Exists(OldFilePath))
            {
                IScrapper townComparer = new TownScrapper();
                townComparer.GetContentInJson(OldFilePath);

                if (IScrapper.JsonContent == null)
                {
                    IScrapper.GetHtmlFromPage();
                    IScrapper.GetContentInJson(Path);
                    newFile = Directory.GetFiles(Path)[0];
                }

                if (IScrapper.JsonContent.Equals(townComparer.JsonContent))
                {
                    File.Delete(newFile);
                    return false;
                }
            }
            string fileName = Directory.GetFiles(path)[0].Substring(newFile.LastIndexOf("\\") + 1);
            File.Delete(oldFile);
            File.Move(newFile, oldFilePath + "\\" + fileName);

            return true;
        }
    }




}
