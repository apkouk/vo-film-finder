using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersioOriginalTester.Interfaces;

namespace VersioOriginalTester.Classes
{

    public class Website
    {
        private string name;
        private string tag;
        private string url;
        private string fileNameSaving;
        private string content;
        private DateTime lastExecution;

        //Add models folder and move into it

        public Website()
        {
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public string FilenameSaving
        {
            get { return fileNameSaving; }
            set { fileNameSaving = value; }
        }

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public DateTime LastExecution
        {
            get
            {
                if (lastExecution == null)
                    lastExecution = DateTime.Now;
                return lastExecution;
            }
            set { lastExecution = value; }
        }

    }
}
