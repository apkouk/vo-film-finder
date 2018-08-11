using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinevoScraper.Models;

namespace CinevoScraper.Helpers
{
    public static class CinevoParameters
    {
        public static readonly CinevoSettings CinevoSettings = CinevoMongoDb.GetSettings();
        
    }
}
