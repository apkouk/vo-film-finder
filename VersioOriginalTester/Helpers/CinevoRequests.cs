using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VersioOriginalTester.Helpers
{
    public class CinevoRequests
    {
        public static string GetContent(string url)
        {
            //Move time sleeping in appconfig
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Executing request to " + url);
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string content = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return content;
        }
    }
}
