using System;
using System.IO;
using System.Net;
using System.Threading;

namespace CinevoScraper.Helpers
{
    public class CinevoRequests
    {
        public static string GetContent(string url)
        {
            //Move time sleeping in appconfig
            Thread.Sleep(RandomMilliseconds());
            Console.WriteLine(url);
            var request = WebRequest.Create(url);
            request.Method = "GET";
            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            if (stream != null)
            {
                var reader = new StreamReader(stream);
                var content = reader.ReadToEnd();
                reader.Close();
                response.Close();
                return content;
            }
            Console.WriteLine("NO CONTENT");
            return "NO CONTENT";
        }

        private static int RandomMilliseconds()
        {
            return new Random().Next(2000, 10000);
        }
    }
}