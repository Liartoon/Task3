using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class GetCallDataEventArgs
    {
        public string CallerNumber { get; private set; }
        public Comparison<CallData> OrderBy { get; private set; }
        public List<CallData> Calls { get; set; }

        public GetCallDataEventArgs(string callerNumber, Comparison<CallData> orderBy)
        {
            CallerNumber = callerNumber;
            OrderBy = orderBy;
        }
    }
}
