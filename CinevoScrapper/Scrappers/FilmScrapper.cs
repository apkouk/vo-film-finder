using CinevoScrapper.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CinevoScrapper.Helpers;
using CinevoScrapper.Models;

namespace CinevoScrapper.Scrappers
{
    public class FilmSInfocrapper 
    {
        //public string Url { get; set; }
        //public string HtmlContent { get; set; }
        //public string JsonContent { get; set; }
        //public bool ForceRequest { get; set; }
        //public string Path { get; set; }
        //public string PathProcessed { get; set; }

     
        //public bool HasChanged()
        //{
        //    try
        //    {
        //        if (!Directory.Exists(PathProcessed))
        //        {
        //            Directory.CreateDirectory(PathProcessed);
        //            return true;
        //        }

        //        var file = new CinevoFiles
        //        {
        //            Scrapper = this,
        //            OldFilePath = PathProcessed,
        //            Path = Path,
        //            HasChanged = false,
        //            Type = CinevoEnums.PageTypes.FilmsCinema
        //        };

        //        file.MakeComparision();
        //        Console.WriteLine("CINEVO FILM SCRAPPER: Has content changed? => " + file.HasChanged);
        //        return file.HasChanged;
        //    }
        //    catch (Exception ex)
        //    {
        //        new Error().SendError(ex);
        //        return false;
        //    }
        //}

        //public void GetHtmlFromUrl()
        //{
        //    if (ForceRequest)
        //    {
        //        HtmlContent = CinevoRequests.GetContent(Url).Trim().TrimEnd().TrimStart();
        //        CinevoFiles.SaveToFile(Path, CinevoEnums.PageTypes.FilmsCinema.ToString(), "html", HtmlContent);
        //    }
        //}
        

        //public void GetContentInJson(string path)
        //{
        //    var addLine = false;
        //    if (Directory.GetFiles(path).Select(x => x.EndsWith(".html")).Count() == 1)
        //    {
        //        var filePath = Directory.GetFiles(path)[0];
        //        var fileReader = new StreamReader(filePath);

        //        string line;
        //        Films = new List<Town>();
        //        while ((line = fileReader.ReadLine()) != null)
        //        {
        //            //if (line.Contains("</select>")) break;

        //            //if (line.Contains("id=\"dropdown-listado-poblacion\" class=\"form-control\"")) addLine = true;
        //            //if (addLine)
        //            //    if (line.Contains("data-name"))
        //            //        Films.Add(ConvertToObject(line));
        //        }

        //        fileReader.Close();
        //        fileReader.Dispose();
        //    }
        //    JsonContent = JsonConvert.SerializeObject(Films).Trim().TrimEnd().TrimStart();
        //    Console.WriteLine("CINEVO FILM SCRAPPER: JsconContent added...");
        //    Console.WriteLine("CINEVO FILM SCRAPPER: " + JsonContent.Substring(0, 50));
        //}

        //private Town ConvertToObject(string lineHtml)
        //{
        //    try
        //    {
        //        //var town = new Town
        //        //{
        //        //    Id = CinevoStrings.GetChunk(lineHtml, "value=\"", "data-name", "\""),
        //        //    Name = CinevoStrings.GetChunk(lineHtml, "/\">", "</a>"),
        //        //    Tag = CinevoStrings.GetChunk(lineHtml, "data-name=\"", "\" >"),
        //        //    Url = CinevoStrings.GetChunk(lineHtml, "<a href=\"", "\">")
        //        //};
        //        //return town;
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        new Error().SendError(ex);
        //        return null;
        //    }
        //}

        //public List<Town> Films { get; set; }
    }
}