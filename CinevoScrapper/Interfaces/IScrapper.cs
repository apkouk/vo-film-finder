using System.Threading.Tasks;

namespace CinevoScrapper.Interfaces
{
    public interface IScrapper 
    {
        string JsonContent { get; }
        string Path { get; }
        string PathProcessed { get; }
      
        void GetHtmlFromUrl();
        void GetContentInJson(string path);
        bool SaveToDb();
    }
}