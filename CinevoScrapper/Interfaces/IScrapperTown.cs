using System.Collections.Generic;
using CinevoScrapper.Models;

namespace CinevoScrapper.Interfaces
{
    public interface IScrapperTown : IScrapper
    {
        bool HasChanged();
        List<Town> Towns { get; set; }
    }
}
