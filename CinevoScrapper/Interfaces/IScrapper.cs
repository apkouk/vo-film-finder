using System.Collections.Generic;
using CinevoScrapper.Models;

namespace CinevoScrapper.Interfaces
{
    public interface IScrapper 
    {
        string Url { get; set; }
        string HtmlContent { get; set; }
        string JsonContent { get; set; }
        string Path { get; set; }
        string PathProcessed { get; set; }
        bool ForceRequest { get; set; }

        bool HasChanged();
        void GetHtmlFromUrl();
        void GetContentInJson(string path);
    }
}