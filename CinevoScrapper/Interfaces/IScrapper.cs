using System.Threading.Tasks;

namespace CinevoScrapper.Interfaces
{
    public interface IScrapper 
    {
        string JsonContent { get; }
        string Path { get; }
        string PathProcessed { get; }

        bool HasChanged();
        void GetHtmlFromUrl();
        void GetContentInJson(string path);
        bool SaveToDb();
    }
}