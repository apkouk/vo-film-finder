using System.Collections.Generic;
using CinevoScraper.Models;

namespace CinevoScraper.Interfaces
{
    public interface IScraperTown : IScraper
    {
        bool HasChanged();
        List<Town> Towns { get; set; }
    }
}
