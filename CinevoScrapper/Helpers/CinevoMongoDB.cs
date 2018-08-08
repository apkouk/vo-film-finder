using CinevoScrapper.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinevoScrapper.Helpers
{
    public static class CinevoMongoDb
    {

        
        private const string ConnectionString = "mongodb://127.0.0.1:27017";
        //private const string ConnectionString = "mongodb+srv://cinevo:EB03HsKpqj0GQ0Bb@cinevo-jg8gu.mongodb.net/test";
        private const string DataBase = "cinevo";


        public static bool SaveTownsInDd(List<Town> towns)
        {
            try
            {
                var client = new MongoClient(ConnectionString);
                IMongoDatabase db = client.GetDatabase(DataBase);
                db.DropCollection("Towns");
                db.CreateCollection("Towns");
                var collection = db.GetCollection<BsonDocument>("Towns");

                List<BsonDocument> bsons = new List<BsonDocument>();

                foreach (Town obj in towns)
                {
                    var document = new BsonDocument
                    {
                        {"Id", new BsonInt64(Convert.ToInt64(obj.Id))},
                        {"Name", new BsonString(obj.Name)},
                        {"Tag", new BsonString(obj.Tag)},
                        {"Url", new BsonString(obj.Url)}
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

        public static bool SaveCinemasInDb(List<Cinema> cinemas)
        {
            var client = new MongoClient(ConnectionString);
            IMongoDatabase db = client.GetDatabase(DataBase);
            db.DropCollection("Cinemas");
            db.CreateCollection("Cinemas");
            var collection = db.GetCollection<BsonDocument>("Cinemas");

            List<BsonDocument> cinevoDocuments = new List<BsonDocument>();

            foreach (Cinema obj in cinemas)
            {
                if (obj.OriginalVersionFilms != null)
                {
                    var arrayFilms = new BsonArray();
                    foreach (Film objFilm in obj.OriginalVersionFilms)
                    {
                        var arrayDays = new BsonArray();
                        foreach (Day objDay in objFilm.Days)
                        {
                            var arrayTimes = new BsonArray();
                            foreach (string filmTIme in objDay.Times)
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
                            {"Actors", objFilm.Actors ?? string.Empty},
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


                    var cinema = new BsonDocument
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
                    cinevoDocuments.Add(cinema);
                }
            }

            collection.InsertManyAsync(cinevoDocuments.AsEnumerable()).Wait();
            var count = collection.AsQueryable().Count();
            return cinemas.Count == count;
        }
    }
}