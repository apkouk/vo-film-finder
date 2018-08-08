using System.Collections.Generic;
using CinevoScraper.Models;

namespace CinevoScraper.Interfaces
{
    public interface IScraperFilms : IScraper
    {
        List<Film> Films { get; set; }
    }
}