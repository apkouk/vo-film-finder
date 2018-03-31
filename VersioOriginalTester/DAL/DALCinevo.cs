using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersioOriginalTester.DAL
{
    public class DALCinevo
    {
        public DALCinevo()
        {

        }
        public void Connect()
        {
            var connectionString = "mongodb://localhost:27017";
            var newClient = new MongoClient(connectionString);
        }
    }
}
