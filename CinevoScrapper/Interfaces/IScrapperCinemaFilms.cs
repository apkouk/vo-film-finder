using System.Collections.Generic;
using CinevoScrapper.Models;

namespace CinevoScrapper.Interfaces
{
    public interface IScrapperFilms : IScrapper
    {
        List<Film> Films { get; set; }
    }
}