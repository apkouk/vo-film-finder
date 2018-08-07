using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CinevoScrapper.Models
{
    public class Town
    {
        private List<Cinema> _cinemas = new List<Cinema>();

        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement]
        public string Id { get; set; }
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public string Url { get; set; }
        [BsonElement]
        public string Tag { get; set; }

      
    }
}