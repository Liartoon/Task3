using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class EndEventArgs
    {
        public string CallerNumber { get; private set; }
        public string CallingNumber { get; set; }

        public EndEventArgs(string callerNumber,string callingNumber)
        {
            CallerNumber = callerNumber;
            CallingNumber = callingNumber;
        }
    }
}
