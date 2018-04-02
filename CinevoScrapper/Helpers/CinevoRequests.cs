using System;
using System.IO;
using System.Net;
using System.Threading;

namespace CinevoScrapper.Helpers
{
    public class CinevoRequests
    {
        public static string GetContent(string url)
        {
            //Move time sleeping in appconfig
            Thread.Sleep(2000);
            Console.WriteLine("Executing request to " + url);
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
            return "NO CONTENT";
        }
    }
}