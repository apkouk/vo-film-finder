using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CinevoScraper.Models
{
    public class Film
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public string Durantion { get; set; }
        [BsonElement]
        public string Genre { get; set; }
        [BsonElement]
        public string FirstShown { get; set; }
        [BsonElement]
        public string Director { get; set; }
        [BsonElement]
        public string Actors { get; set; }
        [BsonElement]
        public string Description { get; set; }
        [BsonElement]
        public string FilmUrl { get; set; }
        [BsonElement]
        public List<Day> Days { get; set; }
        [BsonElement]
        public string Image { get; set; }
        [BsonElement]
        public string Video { get; set; }
        [BsonElement]
        public string Version { get; set; }
        [BsonElement]
        public string Tag { get; set; }
        [BsonElement]
        public string Country { get; set; }

        public Film()
        {
            Days = new List<Day>();
        }

    }



}