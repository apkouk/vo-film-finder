using MongoDB.Driver;

namespace CinevoScrapper.MongoDB
{
    public class CinevoMongoDb
    {
        public void Connect()
        {
            var connectionString = "mongodb://localhost:27017";
            var newClient = new MongoClient(connectionString);
        }
    }
}