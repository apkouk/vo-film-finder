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

        public static bool SaveCinemasInDd(List<Cinema> cinemas)
        {
            var connectionString = "mongodb+srv://cinevo:EB03HsKpqj0GQ0Bb@cinevo-jg8gu.mongodb.net/test";
            var client = new MongoClient(connectionString);
            IMongoDatabase db = client.GetDatabase("cinevo");
            db.DropCollection("Cinemas");
            db.CreateCollection("Cinemas");
            var collection = db.GetCollection<BsonDocument>("Cinemas");

            List<BsonDocument> bsons = new List<BsonDocument>();

            foreach (Cinema obj in cinemas)
            {
                if (obj.OriginalVersionFilms != null)
                {
                    var arrayFilms = new BsonArray();
                    foreach (var objFilm in obj.OriginalVersionFilms)
                    {
                        var arrayDays = new BsonArray();
                        foreach (var objDay in objFilm.Days)
                        {
                            var arrayTimes = new BsonArray();
                            foreach (var filmTIme in objDay.Times)
                            {
                                var time = new BsonDocument
                                {
                                    {"Time", filmTIme ?? string.Empty},
                                };
                                arrayTimes.Add(time);
                            }


                            var day = new BsonDocument
                            {
                                {"Day", objDay.DayOfWeek ?? string.Empty},
                                {"Times", arrayTimes}
                            };
                            arrayDays.Add(day);
                        }


                        var film = new BsonDocument
                        {
                            {"Name", objFilm.Name ?? string.Empty},
                            {"Durantion", objFilm.Durantion ?? string.Empty},
                            {"Genre", objFilm.Genre ?? string.Empty},
                            {"FirstShown", objFilm.FirstShown ?? string.Empty},
                            {"Director", objFilm.Name ?? string.Empty},
                            {"Actors", objFilm.Durantion ?? string.Empty},
                            {"Description", objFilm.Genre ?? string.Empty},
                            {"FilmUrl", objFilm.FilmUrl ?? string.Empty},
                            {"Image", objFilm.Image ?? string.Empty},
                            {"Video", objFilm.Video ?? string.Empty},
                            {"Version", objFilm.Version ?? string.Empty},
                            {"Tag", objFilm.Tag ?? string.Empty},
                            {"Country", objFilm.Country ?? string.Empty},
                            {"Sessions", arrayDays}
                        };
                        arrayFilms.Add(film);
                    }


                    var document = new BsonDocument
                    {
                        {"Id", obj.CinemaId ?? string.Empty},
                        {"Name", obj.Name ?? string.Empty},
                        {"Tag", obj.Tag ?? string.Empty},
                        {"Address", obj.Address ?? string.Empty},
                        {"Telephone", obj.Telephone ?? string.Empty},
                        {"Url", obj.Url ?? string.Empty},
                        {"NightPasses ", obj.NightPasses  ?? string.Empty},
                        {"MorningPasses ", obj.MorningPasses  ?? string.Empty},
                        {"CheapDay ", obj.CheapDay  ?? string.Empty},
                        {"OnlineTickets ", obj.OnlineTickets  ?? string.Empty},
                        {"MapUrl ", obj.MapUrl  ?? string.Empty},
                        {"TownId", obj.TownId ?? string.Empty},
                        {"Town", obj.Town ?? string.Empty},
                        {"Films", arrayFilms }
                    };
                    bsons.Add(document);
                }
            }

            collection.InsertManyAsync(bsons.AsEnumerable()).Wait();
            var count = collection.AsQueryable().Count();
            return cinemas.Count == count;
        }
    }
}