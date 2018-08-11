using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CinevoScraper.Models
{
    public class CinevoSettings
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement]
        public string PathCinemas { get; set; }
        [BsonElement]
        public string PathFilms { get; set; }
        [BsonElement]
        public string PathCinemasIndex { get; set; }
        [BsonElement]
        public string PathImages { get; set; }
        [BsonElement]
        public bool ForceRequest { get; set; }
    }
}
