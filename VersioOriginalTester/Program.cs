using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersioOriginalTester.Interfaces;
using VersioOriginalTester.Classes;

namespace VersioOriginalTester
{
    class Program
    {
        static void Main(string[] args)
        {
            FilmFinder filmFinder = new FilmFinder(LoadWebsites()); 
        }

        private static ArrayList LoadWebsites()
        {
            ArrayList webs = new ArrayList();
            Website website = new Website();
            website.Name = "ElPeriodico";
            website.Tag = "elperiodico";
            website.FilenameSaving = "elperiodico";
            website.Url = "https://cartelera.elperiodico.com/cines/";
            website.LastExecution = DateTime.Now.AddHours(-1);
            webs.Add(website);
            return webs;
        }
    }
}
