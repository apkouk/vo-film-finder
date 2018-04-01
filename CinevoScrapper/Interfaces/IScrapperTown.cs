using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinevoScrapper.Models;

namespace CinevoScrapper.Interfaces
{
    public interface IScrapperTown : IScrapper
    {
        List<Town> Towns { get; set; }
    }
}
