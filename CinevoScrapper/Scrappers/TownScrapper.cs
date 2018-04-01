﻿using CinevoScrapper.Helpers;
using CinevoScrapper.Interfaces;
using CinevoScrapper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CinevoScrapper.Scrappers
{
    public class TownScrapper : IScrapperTown   
    {
        public string HtmlContent { get; set; }
        public string JsonContent { get; set; }
        public bool ForceRequest { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public string PathProcessed { get; set; }
        public List<Town> Towns { get; set; }

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

        public void GetContentInJson(string path)
        {
            var addLine = false;
            List<Town> towns = new List<Town>();

            Console.WriteLine("Getting content to json from " + path);

            if (Directory.GetFiles(path).Select(x => x.EndsWith(".html")).Count() == 1)
            {
                var filePath = Directory.GetFiles(path)[0];
                var fileReader = new StreamReader(filePath);

                string line;
                while ((line = fileReader.ReadLine()) != null)
                {
                    if (line.Contains("</select>")) break;

                    if (line.Contains("id=\"dropdown-listado-poblacion\" class=\"form-control\"")) addLine = true;
                    if (addLine)
                        if (line.Contains("data-name"))
                            towns.Add(ConvertToObject(line));
                }

                Towns = towns;
                fileReader.Close();
                fileReader.Dispose();
            }
      
            JsonContent = JsonConvert.SerializeObject(towns).Trim().TrimEnd().TrimStart();
        }

        

        bool IScrapper.HasChanged()
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
                    Scrapper = this,
                    OldFilePath = PathProcessed,
                    Path = Path,
                    HasChanged = false
                };

                file.MakeComparision();
                return file.HasChanged;
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
                return false;
            }
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