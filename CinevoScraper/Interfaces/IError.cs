using System;

namespace CinevoScraper.Interfaces
{
    public interface IError
    {
        void SendError(Exception ex);
    }
}