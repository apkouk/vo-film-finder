using CinevoScrapper.Helpers;
using CinevoScrapper.Interfaces;
using CinevoScrapper.Models;
using System;
using System.IO;
using System.Linq;

namespace CinevoScrapper.Scrappers
{
    public class FilmInfoScrapper : IScrapperFilmInfo
    {
        public string Url { get; set; }
        public string HtmlContent { get; set; }
        public string JsonContent { get; }
        public string Path { get; set; }
        public string PathProcessed { get; set; }
        public bool ForceRequest { get; set; }

        public Film Film { get; set; }
        public string Tag { get; set; }

        private const string StartActors = "REPARTO:";
        private const string StartDirector = "DIRECCIÓN:";
        private const string StartDuration = "DURACIÓN:";
        private const string StartGenre = "GÉNERO:";
        private const string StartDescription = "SINOPSIS:";
        private const string StartEstreno = "ESTRENO:";
        private const string StartCountry = "PAÝS:";

        public void GetHtmlFromUrl()
        {
            try
            {
                if (ForceRequest)
                {
                    HtmlContent = CinevoRequests.GetContent(Url).Trim().TrimEnd().TrimStart();
                    CinevoFiles.SaveToFile(Path, Tag, "html", HtmlContent);
                }
                GetContentInJson(Path);
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
            }
        }



        public void GetContentInJson(string path)
        {
            string files = Directory.GetFiles(path).ToList().First(x => x.Contains(Film.Tag));

            if (!string.IsNullOrEmpty(files))
            {
                var fileReader = new StreamReader(files);
                string line;
          
                bool updatingDescription = false;

                while ((line = fileReader.ReadLine()) != null)
                {
                    char tab = '\u0009';
                    line = line.Replace(tab.ToString(), "");
                    
                    //---------------

                    if (updatingDescription)
                        Film.Description = CinevoStrings.StripHtml(line).TrimStart();

                    if (line.Contains(StartDescription))
                        updatingDescription = true;

                    if (!String.IsNullOrEmpty(Film.Description))
                        updatingDescription = false;
                    
                    //---------------

                    if (line.Contains(StartActors))
                        Film.Actors = CinevoStrings.StripHtml(line).Replace(StartActors, string.Empty).TrimStart();

                    if (line.Contains(StartDirector))
                        Film.Director = CinevoStrings.StripHtml(line).Replace(StartDirector, string.Empty).TrimStart();

                    if (line.Contains(StartEstreno))
                        Film.FirstShown = CinevoStrings.StripHtml(line).Replace(StartEstreno, string.Empty).TrimStart();

                    if (line.Contains(StartGenre))
                        Film.Genre = CinevoStrings.StripHtml(line).Replace(StartGenre, string.Empty).TrimStart();

                    if (line.Contains(StartDuration))
                        Film.Durantion = CinevoStrings.StripHtml(line).Replace(StartDuration, string.Empty).TrimStart();

                    if (line.Contains(StartCountry))
                        Film.Country = CinevoStrings.StripHtml(line).Replace(StartCountry, string.Empty).TrimStart();

                }
                fileReader.Close();
                fileReader.Dispose();
            }
        }
        
        public bool SaveToDb()
        {
            throw new NotImplementedException();
        }
    }



}
