using System.Collections.Generic;
using CinevoScrapper.Models;

namespace CinevoScrapper.Interfaces
{
    public interface IScrapperTown : IScrapper
    {
        List<Town> Towns { get; set; }
    }
}
