using System;
using CinevoScraper.Interfaces;

namespace CinevoScraper.Classes
{
    public class Error : IError
    {
        public void SendError(Exception ex)
        {
            Console.WriteLine(ex + "|-----|" + ex.StackTrace);
        }
    }
}