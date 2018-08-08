using System.Collections.Generic;
using CinevoScraper.Models;

namespace CinevoScraper.Interfaces
{
    public interface IScraperCinema: IScraper 
    {
        List<Cinema> Cinemas { get; set; }
    }
}
