using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersioOriginalFinder
{   

    public class Website : IObject
    {
        private bool execute;
        private DateTime lastExecution;
        private const int TIME = 1;


        public Website()
        {
            if (ExecuteScrapper())
            {
               
            } 
        }

        private bool ExecuteScrapper()
        {
            try
            {
                if(LastExecution < DateTime.Now.AddHours(TIME))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
               
        public bool Execute
        {
            get { return execute; }
            set { execute = value; }
        }

        public DateTime LastExecution
        {
            get { return lastExecution; }
            set { lastExecution = value; }
        }
        

        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        
    }
}
