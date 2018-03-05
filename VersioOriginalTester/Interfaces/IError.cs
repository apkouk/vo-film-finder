using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersioOriginalTester.Interfaces
{
    public interface IError
    {
        void SendError(Exception ex);
    }
}


