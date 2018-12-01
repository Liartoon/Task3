using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class CallData
    {
        public string CallerNumber { get; set; }
        public string CallingNumber { get; set; }
        public int Cost { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public CallData(string callerNumber, string callingNumber)
        {
            CallerNumber = callerNumber;
            CallingNumber = callingNumber;
            Cost = 0;
            StartTime = DateTime.Now;
        }

        public CallData(string callerNumber, string callingNumber, DateTime dateEnd)
        {
            CallerNumber = callerNumber;
            CallingNumber = callingNumber;
            Cost = 0;
            EndTime = dateEnd;
        }

        public CallData(string callerNumber, string callingNumber,int cost, DateTime dateStart, DateTime dateEnd)
        {
            CallerNumber = callerNumber;
            CallingNumber = callingNumber;
            Cost = cost;
            StartTime = dateStart;
            EndTime = dateEnd;
        }

        public override bool Equals(object obj)
        {
            if ((CallerNumber==(obj as CallData).CallerNumber)&&(CallingNumber == (obj as CallData).CallingNumber)
                &&(StartTime == (obj as CallData).StartTime))
            {
                return true;
            }
            return false;
        }
    }
}
