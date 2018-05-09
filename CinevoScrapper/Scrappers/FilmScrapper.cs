﻿using CinevoScrapper.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CinevoScrapper.Helpers;
using CinevoScrapper.Models;

namespace CinevoScrapper.Scrappers
{
    public class FilmScrapper : IScrapperFilms
    {
        public string Url { get; set; }
        public string HtmlContent { get; set; }
        public string JsonContent { get; set; }
        public bool ForceRequest { get; set; }
        public string Path { get; set; }
        public string PathProcessed { get; set; }
        public List<Film> Films { get; set; }
        public Cinema Cinema { get; set; }
        

        public void GetHtmlFromUrl()
        {
            try
            {
                if (ForceRequest)
                {
                    HtmlContent = CinevoRequests.GetContent(Url).Trim().TrimEnd().TrimStart();
                    CinevoFiles.SaveToFile(Path, CinevoEnums.PageTypes.FilmsCinema.ToString(), "html", HtmlContent);
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
            if (Directory.GetFiles(path).Select(x => x.EndsWith(".html")).Count() == 1)
            {
                var filePath = Directory.GetFiles(path)[0];
                var fileReader = new StreamReader(filePath);

                var linesPerCinema = new ArrayList();
                Films = new List<Film>();
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
                            if (counter - cinemaAdded == 0)
                            {
                                Films.Add(ConvertToObject(linesPerCinema));
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

                fileReader.Close();
                fileReader.Dispose();
            }
            JsonContent = JsonConvert.SerializeObject(Films).Trim().TrimEnd().TrimStart();
            Console.WriteLine("CINEVO CINEMA FILES SCRAPPER: JsconContent added...");
            Console.WriteLine("CINEVO CINEMA FILES SCRAPPER: " + JsonContent.Substring(0, 50));
        }

        public bool SaveToDb()
        {
            throw new NotImplementedException();
        }

        private Film ConvertToObject(ArrayList linesPerCinema)
        {
            try
            {
                var cinema = new Film();
                foreach (string lineHtml in linesPerCinema)
                {
                    //if (lineHtml.Contains("col-xs-12 col-sm-6 col-md-6 info-cine"))
                    //    cinema.TownId = CinevoStrings.GetChunk(lineHtml, "data-poblacion=\"", "\">");
                    //if (lineHtml.Contains("href"))
                    //{
                    //    cinema.Name = CinevoStrings.GetChunk(lineHtml, "\">", "</a>");
                    //    cinema.Url = CinevoStrings.GetChunk(lineHtml, "href=\"", "\">");
                    //    cinema.Tag = cinema.Name.ToLower().Replace(' ', '-');
                    //}

                    //if (lineHtml.EndsWith("</p>") && cinema.Address != null)
                    //    cinema.Town = lineHtml.Replace(" ", "").Replace("</p>", "");
                    //if (lineHtml.Contains("DIRECCIÓN"))
                    //    cinema.Address = CinevoStrings.GetChunk(lineHtml, "</strong>", "</p>").TrimStart();
                    //if (lineHtml.Contains("TEL"))
                    //    cinema.Telephone = CinevoStrings.GetChunk(lineHtml, "</strong>", "</p>").TrimStart();
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