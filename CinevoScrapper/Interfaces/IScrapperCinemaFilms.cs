using System.Collections.Generic;
using CinevoScrapper.Models;

namespace CinevoScrapper.Interfaces
{
    public interface IScrapperCinemaFilms : IScrapper
    {
        List<Film> Films { get; set; }
    }
}