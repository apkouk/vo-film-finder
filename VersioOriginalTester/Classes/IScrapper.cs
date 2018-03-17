using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersioOriginalTester.Classes
{
    public interface IScrapper
    {      
        string URL { get; set; }
        string HtmlContent { get; set; }
        
        bool ExecuteScrapper();
        void GetHtmlFromPage();
        void GetContentInJson();    
    }
}
