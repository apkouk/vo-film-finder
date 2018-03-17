using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace VersioOriginalTester.Classes
{
    public class VenuePage : IScrapper
    {
        private string url;
        private string content;
        public string venuesJson;
        private const string PATH = @"..\..\HtmlCinemas\";
        private const string FILE_NAME = "Venue";
        public List<Venue> venues = new List<Venue>();
      

        public VenuePage()
        {
        }
        
        public List<Venue> Venues
        {
            get { return venues; }
            set { venues = value; }
        }
        public string HtmlContent
        {
            get { return content; }
            set { content = value; }
        }
        public string URL
        {
            get { return url; }
            set { url = value; }
        }
        public string VenuesJson
        {
            get { return venuesJson; }
            set { venuesJson = value; }
        }        

        public void GetHtmlFromPage()
        {
            try
            {
                WebRequest request = WebRequest.Create(URL);
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string content = reader.ReadToEnd();
                reader.Close();
                response.Close();

                SaveToHtmlFile();

                if (content.Equals(string.Empty))
                    HtmlContent = "NO_CONTENT";
                HtmlContent = content;
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
                HtmlContent = ex.ToString();
            }
        }      
        
        private void SaveToHtmlFile()
        {
            if (!Directory.Exists(PATH))
                Directory.CreateDirectory(PATH);
            System.IO.File.WriteAllText(PATH + FILE_NAME + "_" + DateTime.Now.ToLongDateString() + ".html", HtmlContent);
        }

        public bool ExecuteScrapper()
        {
            try
            {
                return HasChanged();
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
                return false;
            }
        }

        private Venue ConvertToObject(string venueHtml)
        {
            try
            {
                Venue venue = new Venue();
                venue.Id = Stringizer.GetChunk(venueHtml, "value=\"", "data-name", "\"");
                venue.Name = Stringizer.GetChunk(venueHtml, "/\">", "</a>");
                venue.Tag = Stringizer.GetChunk(venueHtml, "data-name=\"", "\" >");
                venue.URL = Stringizer.GetChunk(venueHtml, "<a href=\"", "\">");
                return venue;
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
                return null;
            }
        }

        private bool HasChanged()
        {
            if (Directory.Exists(PATH) && Directory.EnumerateFiles(PATH).Count() == 0)
            {
                return true;
            }           
            return CompareWithDB();
        }

        private bool CompareWithDB()
        {
            GetContentInJson();
            string jsonFromDb = string.Empty;
            string jsonFromHtml = this.VenuesJson;

            return false;
        }


        public void GetContentInJson()
        {
            string line = string.Empty;
            int counter = 0;
            bool addLine = false;
            ArrayList venuesHtml = new ArrayList();

            string filePath = Directory.GetFiles(PATH)[0];
            StreamReader fileReader = new StreamReader(filePath);
            while ((line = fileReader.ReadLine()) != null)
            {
                if (line.Contains("</select>"))
                {
                    break;
                }
                if (line.Contains("id=\"dropdown-listado-poblacion\" class=\"form-control\""))
                {
                    addLine = true;
                }
                if (addLine)
                {
                    if (line.Contains("data-name"))
                    {
                        venuesHtml.Add(line);
                        System.Console.WriteLine(line);
                    }
                }
                counter++;
            }

            foreach (string venueHtml in venuesHtml)
            {
                Venue venue = ConvertToObject(venueHtml);
                Venues.Add(venue);
            }
            VenuesJson = JsonConvert.SerializeObject(Venues);
          
        }
    }
}
