using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersioOriginalTester.MongoDB
{
    public class CinevoMongoDB
    {
        public CinevoMongoDB()
        {

        }
        public void Connect()
        {
            var connectionString = "mongodb://localhost:27017";
            var newClient = new MongoClient(connectionString);
        }
    }
}
