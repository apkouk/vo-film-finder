using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CinevoScrapper.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace CinevoScrapper.Helpers
{
    public static class CinevoMongoDb
    {
        public static bool SaveTownsInDd(List<Town> towns)
        {
            try
            {
                var connectionString = "mongodb+srv://cinevo:EB03HsKpqj0GQ0Bb@cinevo-jg8gu.mongodb.net/test";
                var client = new MongoClient(connectionString);
                IMongoDatabase db = client.GetDatabase("cinevo");
                db.DropCollection("Towns");
                db.CreateCollection("Towns");
                var collection = db.GetCollection<BsonDocument>("Towns");

                List<BsonDocument> bsons = new List<BsonDocument>();

                foreach (Town obj in towns)
                {
                    var document = new BsonDocument
                    { 
                        {"Id", obj.Id},
                        {"Name", obj.Name},
                        {"Tag", obj.Tag},
                        {"Url", obj.Url}
                    };
                    bsons.Add(document);
                }
             
                collection.InsertManyAsync(bsons.AsEnumerable()).Wait();
                var count = collection.AsQueryable().Count();
                return towns.Count == count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}