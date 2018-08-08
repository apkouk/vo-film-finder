using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CinevoScraper.Models
{
    public class Cinema
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement]
        public string CinemaId { get; set; }
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public string Tag { get; set; }
        [BsonElement]
        public string Address { get; set; }
        [BsonElement]
        public string Telephone { get; set; }
        [BsonElement]
        public string Url { get; set; }
        //public string Info { get; set; }
        //public decimal Lattitude { get; set; }
        //public decimal Longitude { get; set; }
        [BsonElement]
        public string NightPasses { get; set; }
        [BsonElement]
        public string MorningPasses { get; set; }
        [BsonElement]
        public string CheapDay { get; set; }
        [BsonElement]
        public string OnlineTickets { get; set; }
        [BsonElement]
        public string MapUrl{ get; set; }
        [BsonElement]
        public string TownId { get; set; }
        [BsonElement]
        public string Town { get; set; }
        public readonly List<Film> Films = new List<Film>();
        public readonly List<Film> OriginalVersionFilms = new List<Film>();
    }
}