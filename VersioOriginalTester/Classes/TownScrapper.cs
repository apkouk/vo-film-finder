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
    public class TownScrapper : IScrapper
    {
        private const string PATH = @"..\..\HtmlCinemas\";
        private const string PATH_PROCESSED = @"..\..\HtmlCinemas\Processed";
        
        public List<Town> towns = new List<Town>();
        public List<Town> Towns
        {
            get { return towns; }
            set { towns = value; }
        }

        private string htmlContent;
        public string HtmlContent
        {
            get { return htmlContent; }
            set { htmlContent = value; }
        }

        private string jsonContent;
        public string JsonContent
        {
            get { return jsonContent; }
            set { jsonContent = value; }
        }

        private string url;
        public string URL
        {
            get { return url; }
            set { url = value; }
        }

        public TownScrapper() { }

        public void GetHtmlFromPage()
        {
            try
            {                
                HtmlContent = CinevoRequests.GetContent(URL).Trim().TrimEnd().TrimStart();
                CinevoFiles.SaveToFile(PATH, CinevoEnums.PageTypes.Town.ToString(), "html", HtmlContent);

                if (HtmlContent.Equals(string.Empty))
                    HtmlContent = "NO_CONTENT";
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
            }
        }

        private Town ConvertToObject(string lineHtml)
        {
            try
            {
                Town town = new Town();
                town.Id = CinevoStrings.GetChunk(lineHtml, "value=\"", "data-name", "\"");
                town.Name = CinevoStrings.GetChunk(lineHtml, "/\">", "</a>");
                town.Tag = CinevoStrings.GetChunk(lineHtml, "data-name=\"", "\" >");
                town.URL = CinevoStrings.GetChunk(lineHtml, "<a href=\"", "\">");
                return town;
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
                return null;
            }
        }
        
        public void GetContentInJson(string path)
        {
            string line = string.Empty;
            string filePath = string.Empty;
            int counter = 0;
            bool addLine = false;

            Console.WriteLine("Getting content to json from " + path);

            if (Directory.GetFiles(path).Select(x => x.EndsWith(".html")).Count() == 1)
            {
                filePath = Directory.GetFiles(path)[0];
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
                            Towns.Add(ConvertToObject(line));
                        }
                    }
                    counter++;
                }

                fileReader.Close();
                fileReader.Dispose();
            }

            JsonContent = JsonConvert.SerializeObject(Towns).Trim().TrimEnd().TrimStart();
        }

        bool IScrapper.HasChanged()
        {
            try
            {
                if (!Directory.Exists(PATH_PROCESSED))
                {
                    Directory.CreateDirectory(PATH_PROCESSED);
                    return true;
                }

                CinevoFiles file = new CinevoFiles();
                file.IScrapper = this;
                file.OldFilePath = PATH_PROCESSED;
                file.Path = PATH;
                return file.AreFilesDifferent();
            }
            catch (Exception ex)
            {
                new Error().SendError(ex);
                return false;
            }
        }
    }
}




