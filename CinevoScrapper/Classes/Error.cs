using System;
using CinevoScrapper.Interfaces;

namespace CinevoScrapper
{
    public class Error : IError
    {
        public void SendError(Exception ex)
        {
            Console.WriteLine(ex + "|-----|" + ex.StackTrace);
        }
    }
}