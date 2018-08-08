namespace CinevoScrapper.Interfaces
{
    public interface IScrapper 
    {
        string JsonContent { get; }
        void GetHtmlFromUrl();
        void ScrapeHtml(string htmlPath);
        bool SaveToDb();
    }
}