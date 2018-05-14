using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CinevoScrapper.Helpers;
using CinevoScrapper.Interfaces;
using CinevoScrapper.Models;
using Newtonsoft.Json;

namespace CinevoScrapper.Scrappers
{
    public class CinemaScrapper : IScrapperCinema
    {
        private int count = 1;
        public string Url { get; set; }
        public string HtmlContent { get; set; }
        public string JsonContent { get; set; }
        public bool ForceRequest { get; set; }
        public string Path { get; set; }
        public string PathProcessed { get; set; }
        public bool HasChanged { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public bool FindFilms { get; set; }

        bool IScrapperCinema.HasChanged()
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
                    HasChanged = false,
                    Type = CinevoEnums.PageTypes.Cinema
                };

                file.MakeComparision();
                Console.WriteLine("CINEVO CINEMA SCRAPPER: Has content changed? => " + file.HasChanged);
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

        public void GetContentInJson(string path)
        {
            JsonContent = "NO CONTENT";
            var addLine = false;
            string getLastHtmlFullName = CinevoFiles.GetLastHtmlPath(path);

            if (getLastHtmlFullName != null && !getLastHtmlFullName.Equals(string.Empty))
            {
                Cinemas = new List<Cinema>();
                var linesPerCinema = new ArrayList();
                var fileReader = new StreamReader(getLastHtmlFullName);

                string line;
                var counter = 0;
                var cinemaAdded = 2;

                while ((line = fileReader.ReadLine()) != null)
                {
                    if (line.Contains("<!-- /listado CINES -->"))
                        break;

                    if (!line.Trim().Equals(string.Empty) && line.Trim().Length > 20)
                    {
                        if (line.Contains("col-xs-12 col-sm-6 col-md-6 info-cine"))
                        {
                            addLine = true;
                            counter++;
                        }

                        if (addLine)
                        {
                            if (counter - cinemaAdded == 0)
                            {
                                Cinema cinema = ConvertToObject(linesPerCinema);
                                if (cinema.Name != null)
                                    Cinemas.Add(cinema);
                                counter = 0;
                                cinemaAdded = 1;
                                linesPerCinema.Clear();
                                linesPerCinema.Add(line);
                            }
                            else
                            {
                                linesPerCinema.Add(line);
                            }
                        }
                    }
                }

                fileReader.Close();
                fileReader.Dispose();
                JsonContent = JsonConvert.SerializeObject(Cinemas).Trim().TrimEnd().TrimStart();
                Console.WriteLine("CINEVO CINEMA SCRAPPER: JsconContent added...");
            }
        }

        public bool SaveToDb()
        {
            if (HasChanged)
                return CinevoMongoDb.SaveCinemasInDd(Cinemas);
            return false;
        }

        private Cinema ConvertToObject(ArrayList linesPerCinema)
        {
            try
            {
                var cinema = new Cinema();
                cinema.CinemaId = count++.ToString();
                foreach (string lineHtml in linesPerCinema)
                {
                    if (lineHtml.Contains("col-xs-12 col-sm-6 col-md-6 info-cine"))
                        cinema.TownId = CinevoStrings.GetChunk(lineHtml, "data-poblacion=\"", "\">");
                    if (lineHtml.Contains("href"))
                    {
                        cinema.Name = CinevoStrings.GetChunk(lineHtml, "\">", "</a>");
                        cinema.Url = CinevoStrings.GetChunk(lineHtml, "href=\"", "\">");
                        cinema.Tag = cinema.Name.ToLower().Replace(' ', '-');
                    }
                    if (lineHtml.Contains("TEL"))
                        cinema.Telephone = CinevoStrings.GetChunk(lineHtml, "</strong>", "</p>").TrimStart();
                    if (lineHtml.Contains("DIRECCIÓN"))
                        cinema.Address = CinevoStrings.GetChunk(lineHtml, "</strong>", "</p>").TrimStart();
                    if (lineHtml.EndsWith("</p>") && cinema.Address != null)
                        cinema.Town = lineHtml.Replace("</p>", "");
                }
                return cinema;
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
                return null;
            }
        }
    }
}