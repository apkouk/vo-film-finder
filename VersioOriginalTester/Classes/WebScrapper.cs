using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VersioOriginalTester.Interfaces;

namespace VersioOriginalTester.Classes
{
    public class WebScrapper
    {
        private Website website;
        private const int TIME = 1;
        private const string path = @"..\..\HtmlWebsite\";

        public WebScrapper(Website website)
        {
            this.Website = website;

            if(ExecuteScrapper())
            {
                website.Content = GetHtmlFile();
                SaveToHtmlFile();
            }          
        }

        private void SaveToHtmlFile()
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            System.IO.File.WriteAllText(path + website.FilenameSaving + ".html", website.Content);
        }     

        public Website Website
        {
            get { return website; }
            set { website = value; }
        }

        private bool ExecuteScrapper()
        {
            try
            {
                if ((Website.LastExecution < DateTime.Now.AddMinutes(TIME)))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
                return false;
            }
        }

        public string GetHtmlFile()
        {
            try
            {            
                WebRequest request = WebRequest.Create(website.Url);
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string content = reader.ReadToEnd();
                reader.Close();
                response.Close();

                if (content.Equals(string.Empty))
                    return "NO_CONTENT";
                return content;
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
                return "NO_CONTENT_ON_ERROR"; 
            }

        }
    }
}
