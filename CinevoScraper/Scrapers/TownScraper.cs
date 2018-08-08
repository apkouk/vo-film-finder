using System;
using System.Collections.Generic;
using System.IO;
using CinevoScraper.Classes;
using CinevoScraper.Helpers;
using CinevoScraper.Interfaces;
using CinevoScraper.Models;
using Newtonsoft.Json;

namespace CinevoScraper.Scrapers
{
    public class TownScraper : IScraperTown
    {
        public string Url { get; set; }
        public string HtmlContent { get; set; }
        public string JsonContent { get; set; }
        public bool ForceRequest { get; set; }
        public string Path { get; set; }
        public string PathProcessed { get; set; }
        public bool HasChanged { get; set; }
        public List<Town> Towns { get; set; }

        bool IScraperTown.HasChanged()
        {
            try
            {
                if (!Directory.Exists(PathProcessed))
                {
                    Directory.CreateDirectory(PathProcessed);
                    return true;
                }

                var file = new CinevoFiles
                {
                    Scraper = this,
                    OldFilePath = PathProcessed,
                    Path = Path,
                    HasChanged = false,
                    Type = CinevoEnums.PageTypes.Town
                };

                file.MakeComparision();
                Console.WriteLine("CINEVO TOWN SCRAPPER: Has content changed? => " + file.HasChanged);
                HasChanged = file.HasChanged;
                return HasChanged;
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
                return false;
            }
        }

        public void GetHtmlFromUrl()
        {
            try
            {
                if (ForceRequest)
                {
                    HtmlContent = CinevoRequests.GetContent(Url).Trim().TrimEnd().TrimStart();
                    CinevoFiles.SaveToFile(Path, CinevoEnums.PageTypes.Town.ToString(), "html", HtmlContent);
                }
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
            }
        }

        public void ScrapeHtml(string path)
        {
            JsonContent = "NO CONTENT";
            var addLine = false;
            string getLastHtmlFullName = CinevoFiles.GetLastHtmlPath(path);

            if (getLastHtmlFullName != null && !getLastHtmlFullName.Equals(string.Empty))
            {
                Towns = new List<Town>();
                var fileReader = new StreamReader(getLastHtmlFullName);

                string line;
                while ((line = fileReader.ReadLine()) != null)
                {
                    if (line.Contains("</select>"))
                        break;

                    if (line.Contains("id=\"dropdown-listado-poblacion\" class=\"form-control\""))
                        addLine = true;

                    if (addLine)
                        if (line.Contains("data-name"))
                            Towns.Add(ConvertToObject(line));
                }

                fileReader.Close();
                fileReader.Dispose();
            }
            JsonContent = JsonConvert.SerializeObject(Towns).Trim().TrimEnd().TrimStart();
            Console.WriteLine("CINEVO TOWN SCRAPPER: JsconContent added...");
        }

        public bool SaveToDb()
        {
            if (HasChanged)
                return CinevoMongoDb.SaveTownsInDd(Towns);
            return false;
        }

        private Town ConvertToObject(string lineHtml)
        {
            try
            {
                var town = new Town
                {
                    Id = CinevoStrings.GetChunk(lineHtml, "value=\"", "data-name", "\""),
                    Name = CinevoStrings.GetChunk(lineHtml, "/\">", "</a>"),
                    Tag = CinevoStrings.GetChunk(lineHtml, "data-name=\"", "\" >"),
                    Url = CinevoStrings.GetChunk(lineHtml, "<a href=\"", "\">")
                };
                return town;
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
                return null;
            }
        }

    }
}