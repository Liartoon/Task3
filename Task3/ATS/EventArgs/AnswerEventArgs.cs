using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class AnswerEventArgs
    {
        public string CallerNumber { get; private set; }
        public string CallingNumber { get; private set; }
        public CallState CallState { get; private set; }

        public AnswerEventArgs(string callerNumber, string callingNumber, CallState callState)
        {
            CallerNumber = callerNumber;
            CallingNumber = callingNumber;
            CallState = callState;
        }
    }
}
