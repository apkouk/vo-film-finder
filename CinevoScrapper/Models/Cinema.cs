﻿using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CinevoScrapper.Models
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
        public string TownId { get; set; }
        [BsonElement]
        public string Town { get; set; }
        public List<Film> Films = new List<Film>();
    }
}