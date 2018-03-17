using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersioOriginalTester.Classes
{
    public class FilmPage : IScrapper
    {

        private string url;
        private string content;
        private const string PATH = @"..\..\HtmlWebsite\";
        private const string FILE_NAME = "Venue";

        public FilmPage() { }

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

        public bool ExecuteScrapper()
        {
            throw new NotImplementedException();
        }

        public void GetHtmlFromPage()
        {
            throw new NotImplementedException();
        }

    

public void GetContentInJson()
{
 	throw new NotImplementedException();
}
}
}
