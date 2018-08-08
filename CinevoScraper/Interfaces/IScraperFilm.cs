using System.Collections.Generic;
using CinevoScraper.Models;

namespace CinevoScraper.Interfaces
{
    public interface IScraperFilm : IScraper
    {
        List<Cinema> Films { get; set; }
    }
}
