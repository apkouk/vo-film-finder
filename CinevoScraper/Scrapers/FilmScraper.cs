﻿using System;
using System.IO;
using System.Linq;
using CinevoScraper.Classes;
using CinevoScraper.Helpers;
using CinevoScraper.Interfaces;
using CinevoScraper.Models;

namespace CinevoScraper.Scrapers
{
    public class FilmScraper : IScraperFilmInfo
    {
        private string HtmlContent { get; set; }
        public string JsonContent { get; }
        public string Path { get; set; }
        public string PathProcessed { get; set; }
        public bool ForceRequest { get; set; }

        public Film Film { get; set; }
        public Cinema Cinema { get; set; }

        private const string StartActors = "REPARTO:";
        private const string StartDirector = "DIRECCIÓN:";
        private const string StartDuration = "DURACIÓN:";
        private const string StartGenre = "GÉNERO:";
        private const string StartDescription = "SINOPSIS:";
        private const string StartEstreno = "ESTRENO:";
        private const string StartCountry = "PAÝS:";
        private const string StartTrailer = "iframe width";

        public void GetHtmlFromUrl()
        {
            try
            {
                if (ForceRequest && Film.IsOriginalVersion)
                {
                    if (!Directory.Exists(Path))
                        Directory.CreateDirectory(Path);

                    if (new DirectoryInfo(Path).GetFiles().Where(x => x.FullName.Contains(Film.Tag)).ToList().Count == 0)
                    {
                        HtmlContent = CinevoRequests.GetContent(Film.FilmUrl).Trim().TrimEnd().TrimStart();
                        CinevoFiles.SaveToFile(Path, Film.Tag, "html", HtmlContent);
                    }
                }
                ScrapeFilm();
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
            }
        }

        private void ScrapeFilm()
        {
            if (Film.IsOriginalVersion)
            {
                ScrapeHtml(Path);
                Cinema.OriginalVersionFilms.Add(Film);
            }
        }


        public void ScrapeHtml(string path)
        {
            string files = Directory.GetFiles(path).ToList().First(x => x.Contains(Film.Tag));
            Console.WriteLine("Scraping film -> " + Film.Name);
            Console.WriteLine("----\n");
            if (!string.IsNullOrEmpty(files))
            {
                var fileReader = new StreamReader(files);
                string line;

                bool updatingDescription = false;

                while ((line = fileReader.ReadLine()) != null)
                {
                    line = CinevoStrings.RemoveChars(line, '\u0009');

                    //---------------

                    if (updatingDescription)
                        Film.Description = CinevoStrings.StripHtml(line).TrimStart().TrimEnd();

                    if (line.Contains(StartDescription))
                        updatingDescription = true;

                    if (!String.IsNullOrEmpty(Film.Description))
                        updatingDescription = false;

                    //---------------

                    if (line.Contains(StartTrailer) && Film?.Trailer == null)
                        Film.Trailer = CinevoStrings.GetChunk(line, "src=\"", "\" frameborder");

                    if (line.Contains(StartActors))
                        Film.Actors = CinevoStrings.StripHtml(line).Replace(StartActors, string.Empty).TrimStart().TrimEnd();

                    if (line.Contains(StartDirector))
                        Film.Director = CinevoStrings.StripHtml(line).Replace(StartDirector, string.Empty).TrimStart().TrimEnd();

                    if (line.Contains(StartEstreno))
                        Film.FirstShown = CinevoStrings.StripHtml(line).Replace(StartEstreno, string.Empty).TrimStart().TrimEnd();

                    if (line.Contains(StartGenre))
                        Film.Genre = CinevoStrings.StripHtml(line).Replace(StartGenre, string.Empty).TrimStart().TrimEnd();

                    if (line.Contains(StartDuration))
                        Film.Durantion = CinevoStrings.StripHtml(line).Replace(StartDuration, string.Empty).TrimStart().TrimEnd();

                    if (line.Contains(StartCountry))
                        Film.Country = CinevoStrings.StripHtml(line).Replace(StartCountry, string.Empty).TrimStart().TrimEnd();

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
