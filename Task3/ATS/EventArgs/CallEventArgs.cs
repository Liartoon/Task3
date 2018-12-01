using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class CallEventArgs
    {
        public string CallerNumber { get; private set; }
        public string CallingNumber { get; private set; }

        public CallEventArgs(string callerNumber, string callingNumber)
        {
            CallerNumber = callerNumber;
            CallingNumber = callingNumber;
        }
    }
}
