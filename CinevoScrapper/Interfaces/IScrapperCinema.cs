using System.Collections.Generic;
using CinevoScrapper.Models;

namespace CinevoScrapper.Interfaces
{
    public interface IScrapperCinema: IScrapper 
    {
        List<Cinema> Cinemas { get; set; }
    }
}
