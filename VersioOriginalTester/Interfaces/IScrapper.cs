using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersioOriginalTester.Interfaces   
{
    public interface IScrapper
    {
        string URL { get; set; }
        string HtmlContent { get; set; }
        string JsonContent { get; set; }

        bool HasChanged();
        void GetHtmlFromPage();
        void GetContentInJson(string path);
    }
}
