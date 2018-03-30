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

            if(page.HasChanged())
            {
                Console.WriteLine("TO EXECUTE COMPARISION WITH DATABASE");              
                //Use Page.JsonContent to update database               
            }
            Console.WriteLine("NOTHING TO DO, GRAB A BEER!");
        }

        public IScrapper Page
        {
            get { return page; }
            set { page= value; }
        }
       
    }
}
