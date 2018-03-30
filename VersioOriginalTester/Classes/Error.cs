using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersioOriginalTester.Interfaces;

namespace VersioOriginalTester
{
    public class Error : IError
    {
        public void SendError(Exception ex)
        {
            System.Console.WriteLine(ex + "|-----|" + ex.StackTrace);
        }
    }
}
