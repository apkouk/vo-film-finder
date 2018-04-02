using System;

namespace CinevoScrapper.Interfaces
{
    public interface IError
    {
        void SendError(Exception ex);
    }
}