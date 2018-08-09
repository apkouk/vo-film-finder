using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using CinevoScraper.Classes;
using CinevoScraper.Helpers;
using CinevoScraper.Interfaces;
using CinevoScraper.Models;

namespace CinevoScraper.Scrapers
{
    public class CinemaScraper : IScraperFilms
    {
        public string Url { get; set; }
        public string HtmlContent { get; set; }
        public string JsonContent { get; set; }
        public bool ForceRequest { get; set; }
        public string Path { get; set; }
        public string PathProcessed { get; set; }
        public List<Film> Films { get; set; }
        public Cinema Cinema { get; set; }

        private const string EndLocation = "END: onbcn-cinema-location";
        private const string StartLocation = "START: onbcn-cinema-location";
        private const string EndFilms = "START: onbcn-toolbar";
        private const string StartFilms = "Películas en proyeccion";
        private const string EndFilmInfo = "class=\"row\"";
        private const string StartFimInfo = "<figure class=\"thumb\">";

        public void GetHtmlFromUrl()
        {
            try
            {
                if (ForceRequest)
                {
                    HtmlContent = CinevoRequests.GetContent(Cinema.Url).Trim().TrimEnd().TrimStart();
                    CinevoFiles.SaveToFile(Path, Cinema.Tag, "html", HtmlContent);
                }
                ScrapeHtml(Path);
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
            }
        }

        public void ScrapeHtml(string path)
        {
            string files = Directory.GetFiles(path).ToList().First(x => x.Contains(Cinema.Tag));
            Console.WriteLine("Scraping cinema -> " + Cinema.Name);
            Console.WriteLine("----\n");
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

                    if (line.Contains(EndLocation))
                        updatingCinema = false;

                    if (line.Contains(StartLocation))
                        updatingCinema = true;

                    //Films
                    if (updatingFilm)
                        GetFilmInfo(line);

                    if (line.Contains(EndFilms))
                        updatingFilm = false;

                    if (line.Contains(StartFilms))
                        updatingFilm = true;
                }
                fileReader.Close();
                fileReader.Dispose();
            }

            DownloadImages();
        }

        private void DownloadImages()
        {
            foreach (Film film in Cinema.Films)
            {
                if (film.IsOriginalVersion)
                {
                    if (!film.Image.Contains("no-foto-pelicula.gif"))
                    {
                        using (WebClient client = new WebClient())
                        {
                            if (!Directory.Exists(Properties.CinevoScraper.Default.Images))
                                Directory.CreateDirectory(Properties.CinevoScraper.Default.Images);

                            if (new DirectoryInfo(Path).GetFiles().Where(x => x.FullName.Contains(film.Image)).ToList().Count == 0)
                            {
                                client.DownloadFile(new Uri(film.Image.Replace('"', ' ').Trim()), Properties.CinevoScraper.Default.Images + film.Tag + ".jpg");
                            }
                        }
                    }
                }
            }
        }

        private bool _addLine;
        private readonly ArrayList _linesPerFilm = new ArrayList();
        private void GetFilmInfo(string lineHtml)
        {
            if (!lineHtml.Equals(string.Empty))
            {
                char tab = '\u0009';
                lineHtml = lineHtml.Replace(tab.ToString(), "");
                if (lineHtml.Contains(StartFimInfo))
                {
                    _addLine = true;
                }

                if (lineHtml.Contains(EndFilmInfo))
                {
                    _addLine = false;

                    Film film = ConvertToObject(_linesPerFilm);
                    if (film.Name != null)
                        Cinema.Films.Add(film);
                    _linesPerFilm.Clear();
                }

                if (_addLine)
                {
                    {
                        _linesPerFilm.Add(lineHtml);
                    }
                }
            }
        }

        bool _addingAddress;
        private void GetCinemaInfo(string lineHtml)
        {
            if (!lineHtml.Equals(string.Empty))
            {
                if (lineHtml.Contains("col-xs-12 col-sm-12 col-md-3 txt"))
                {
                    _addingAddress = true;
                }

                if (_addingAddress)
                {
                    if (!lineHtml.Trim().Equals("<p>") && !lineHtml.Trim().Equals("</div>") && !lineHtml.Trim().Contains("maps") && !lineHtml.Trim().Contains("col-xs-12 col-sm-12 col-md-3 txt") && lineHtml.Trim().Length > 0)
                    {
                        Cinema.Address = CinevoStrings.RemoveChars(lineHtml, '\t');
                        _addingAddress = false;
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

                if (Cinema.MapUrl != null)
                    SetMapValues();
            }
        }

        private void SetMapValues()
        {
            Cinema.Latitude = CinevoStrings.GetChunk(Cinema.MapUrl, "|", ",");
            Cinema.Longitude = CinevoStrings.GetChunk(Cinema.MapUrl, ",", "&mobile");
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
                Day time = null;
                bool addingDay = false;
                const string baseUrl = "https://cartelera.elperiodico.com";

                foreach (string lineHtml in linesPerFilm)
                {
                    if (!lineHtml.Equals(string.Empty))
                    {
                        if (lineHtml.Contains("<img style"))
                            film.Image = CinevoStrings.GetChunk(lineHtml, "src=\"", "\" alt");

                        if (lineHtml.Contains("Ver película"))
                            film.FilmUrl = baseUrl + CinevoStrings.GetChunk(lineHtml, "href=\"", "\" title");

                        if (lineHtml.Contains("(") && lineHtml.Contains(")") && lineHtml.Contains("V"))
                        {
                            film.Version = lineHtml;
                            film.IsOriginalVersion = IsOriginalVersion(film.Version);
                        }

                        if (lineHtml.Contains("href=\"") && lineHtml.Contains("title=\"") && lineHtml.Contains("class=\""))
                        {
                            film.Name = CinevoStrings.GetChunk(lineHtml, "\">", "</a>");

                            string tagTemp = !film.Name.Equals(string.Empty)
                                ? film.Name.Replace(",", "").Replace(" ", "-").Replace("#", "").TrimEnd().TrimStart().ToLower()
                                : film.Tag = "NOTAG";

                            film.Tag = System.IO.Path.GetInvalidFileNameChars().Aggregate(tagTemp, (current, c) => current.Replace(c.ToString(), string.Empty));
                        }
                        if (lineHtml.Contains("class=\"wrap\""))
                            addingDay = true;

                        if (addingDay)
                        {
                            if (lineHtml.Trim().Contains("<dt>"))
                            {
                                time = new Day { DayOfWeek = CinevoStrings.GetChunk(lineHtml, ">", "</") };
                            }
                            if (lineHtml.Trim().Contains("<dd>"))
                                time?.Times.Add(CinevoStrings.GetChunk(lineHtml, ">", "</"));

                            if (lineHtml.Trim().Contains("</dl>"))
                            {
                                addingDay = false;
                                film.Days.Add(time);
                            }
                        }
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

        private bool IsOriginalVersion(string filmVersion)
        {
            if (filmVersion.Contains("VE") || filmVersion.Contains("VC"))
                return false;
            return true;
        }
    }
}