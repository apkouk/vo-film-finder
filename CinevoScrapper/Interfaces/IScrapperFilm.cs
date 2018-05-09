using System.Collections.Generic;
using CinevoScrapper.Models;

namespace CinevoScrapper.Interfaces
{
    public interface IScrapperFilm : IScrapper
    {
        List<Cinema> Films { get; set; }
    }
}
