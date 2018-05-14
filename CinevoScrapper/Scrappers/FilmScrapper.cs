using CinevoScrapper.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
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

        bool addingAddress = false;

        public void GetHtmlFromUrl()
        {
            try
            {
                if (ForceRequest)
                {
                    HtmlContent = CinevoRequests.GetContent(Cinema.Url).Trim().TrimEnd().TrimStart();
                    CinevoFiles.SaveToFile(Path, Cinema.Tag, "html", HtmlContent);
                }
                GetContentInJson(Path);
                //JsonContent = JsonConvert.SerializeObject(Films).Trim().TrimEnd().TrimStart();
                //Console.WriteLine("CINEVO CINEMA FILES SCRAPPER: JsconContent added...");
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
            }
        }

        public void GetContentInJson(string path)
        {
            string files = Directory.GetFiles(path).ToList().First(x => x.Contains(Cinema.Tag));
            //for (int i = 0; i < files.Length; i++)
            if (!string.IsNullOrEmpty(files))
            {
                Films = new List<Film>();
                
                var fileReader = new StreamReader(files);
                string line;

                bool updatingCinema = false;
                bool updatingFilm = false;

                while ((line = fileReader.ReadLine()) != null)
                {
                    //CinemaInfo
                    if (updatingCinema)
                        GetCinemaInfo(line);

                    if (line.Contains("END: onbcn-cinema-location"))
                        updatingCinema = false;

                    if (line.Contains("START: onbcn-cinema-location"))
                        updatingCinema = true;

                    //Films
                    if (updatingFilm)
                        GetFilmInfo(line);

                    if (line.Contains("START: onbcn-toolbar"))
                        updatingFilm = false;

                    if (line.Contains("Películas en proyeccion"))
                        updatingFilm = true;

                }
         
                //Films.Add(ConvertToObject(linesPerCinema));
          
                fileReader.Close();
                fileReader.Dispose();
            }
        }


        private int _counter;
        private int _filmAdded = 2;
        private bool _addLine = false;
        private readonly ArrayList _linesPerFilm = new ArrayList();
        private void GetFilmInfo(string lineHtml)
        {
            if (!lineHtml.Equals(string.Empty))
            {
                char tab = '\u0009';
                lineHtml = lineHtml.Replace(tab.ToString(), "");
                if (lineHtml.Contains("<figure class=\"thumb\">"))
                {
                    _addLine = true;
                }

                if (lineHtml.Contains("class=\"row\""))
                {
                    _addLine = false;

                    Film film = ConvertToObject(_linesPerFilm);
                    if (film.Name != null)
                        Films.Add(film);
                    _linesPerFilm.Clear();
                }
                //if (addingAddress)
                //{
                //    if (!lineHtml.Trim().Equals("<p>") && !lineHtml.Trim().Equals("</div>") && !lineHtml.Trim().Contains("maps") && !lineHtml.Trim().Contains("col-xs-12 col-sm-12 col-md-3 txt") && lineHtml.Trim().Length > 0)
                //    {
                //        Cinema.Address = CleanAddress(lineHtml);
                //        addingAddress = false;
                //    }
                //}

                if (_addLine)
                {
                    {
                        _linesPerFilm.Add(lineHtml);
                    }
                }


                //if (lineHtml.Contains("Ver película"))
                //    Cinema.Telephone = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                //if (lineHtml.Contains("Venta Golfas:"))
                //    Cinema.NightPasses = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                //if (lineHtml.Contains("Matinales:"))
                //    Cinema.MorningPasses = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                //if (lineHtml.Contains("Día del espectador:"))
                //    Cinema.CheapDay = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                //if (lineHtml.Contains("Venta anticipada:"))
                //    Cinema.OnlineTickets = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                //if (lineHtml.Contains("<img src="))
                //    Cinema.MapUrl = CinevoStrings.GetChunk(lineHtml, "<img src=", ">");
            }
        }


        private void GetCinemaInfo(string lineHtml)
        {
            if (!lineHtml.Equals(string.Empty))
            {
                if (lineHtml.Contains("col-xs-12 col-sm-12 col-md-3 txt"))
                {
                    addingAddress = true;
                }

                if (addingAddress)
                {
                    if (!lineHtml.Trim().Equals("<p>") && !lineHtml.Trim().Equals("</div>") && !lineHtml.Trim().Contains("maps") && !lineHtml.Trim().Contains("col-xs-12 col-sm-12 col-md-3 txt") && lineHtml.Trim().Length > 0)
                    {
                        Cinema.Address = CleanAddress(lineHtml);
                        addingAddress = false;
                    }
                }

                if (lineHtml.Contains("Tel:"))
                    Cinema.Telephone = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                if (lineHtml.Contains("Venta Golfas:"))
                    Cinema.NightPasses = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                if (lineHtml.Contains("Matinales:"))
                    Cinema.MorningPasses = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                if (lineHtml.Contains("Día del espectador:"))
                    Cinema.CheapDay = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                if (lineHtml.Contains("Venta anticipada:"))
                    Cinema.OnlineTickets = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                if (lineHtml.Contains("<img src="))
                    Cinema.MapUrl = CinevoStrings.GetChunk(lineHtml, "<img src=", ">");
            }
        }

        public bool SaveToDb()
        {
            throw new NotImplementedException();
        }

        private Film ConvertToObject(ArrayList linesPerFilm)
        {
            try
            {
                
                var film = new Film();

                bool addingAddress = false;

                foreach (string lineHtml in linesPerFilm)
                {
                    if (!lineHtml.Equals(string.Empty))
                    {
                        //if (addingAddress)
                        //{
                        //    if (!lineHtml.Trim().Equals("<p>") && !lineHtml.Trim().Equals("</div>") && !lineHtml.Trim().Contains("maps") && lineHtml.Trim().Length > 0)
                        //    {
                        //        Cinema.Address = CleanAddress(lineHtml);
                        //        addingAddress = false;
                        //    }
                        //}

                        //if (lineHtml.Contains("col-xs-12 col-sm-12 col-md-3 txt"))
                        //{
                        //    addingAddress = true;
                        //}

                        //if (lineHtml.Contains("Tel:"))
                        //    Cinema.Telephone = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                        //if (lineHtml.Contains("Venta Golfas:"))
                        //    Cinema.NightPasses = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                        //if (lineHtml.Contains("Matinales:"))
                        //    Cinema.MorningPasses = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                        //if (lineHtml.Contains("Día del espectador:"))
                        //    Cinema.CheapDay = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                        //if (lineHtml.Contains("Venta anticipada:"))
                        //    Cinema.OnlineTickets = CinevoStrings.GetChunk(lineHtml, "</strong> ", "</p>");

                        //if (lineHtml.Contains("<img src="))
                        //    Cinema.MapUrl = CinevoStrings.GetChunk(lineHtml, "<img src=", ">");
                    }
                }
                return film;
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
                return null;
            }
        }

        private string CleanAddress(string lineHtml)
        {
            string[] words = lineHtml.Split('\t');
            string result = string.Empty;

            for (int i = 0; i < words.Length; i++)
            {
                if (!words[i].Equals(string.Empty))
                {
                    result += words[i] + " ";
                }
            }
            return result;
        }
    }
}