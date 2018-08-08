namespace CinevoScraper.Interfaces
{
    public interface IScraper 
    {
        string JsonContent { get; }
        void GetHtmlFromUrl();
        void ScrapeHtml(string htmlPath);
        bool SaveToDb();
    }
}