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
        private IScrapper page;

        public WebScrapper(IScrapper page)
        {
            this.Page = page;

            if(page.ExecuteScrapper())
            {
                page.GetHtmlFromPage();               
                page.GetContentInJson();
            }          
        }

        public IScrapper Page
        {
            get { return page; }
            set { page= value; }
        }
       
    }
}
