using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Terminal
    {
        public string CallerNumber { get; private set; }

        public Port Port { get; private set; }

        public event EventHandler<CallEventArgs> CallEvent;
        public event EventHandler<AnswerEventArgs> AnswerToCallEvent;
        public event EventHandler<EndEventArgs> EndCallEvent;

        public event EventHandler<GetCallDataEventArgs> RequestCallDataEvent;
        public event EventHandler<GetCallDataEventArgs> ReturnCallDataEvent;


        public Terminal(string number, Port port)
        {
            CallerNumber = number;
            Port = port;
        }

        public void ConnectToATS()
        {
            if (Port.Connect(this))
            {
                Port.IncomingCallEvent += IncomingCallEvent;
                Port.AnswerCallEvent += AnswerCall;

                Port.ReturnCallDataEvent += ShowCallsList;
            }
        }
        public void DisconnectFromATS()
        {
            if (Port.Disconnect(this))
            {
                Port.IncomingCallEvent -= IncomingCallEvent;
                Port.AnswerCallEvent -= AnswerCall;
            }
        }

        public void Call(string callingNumber)
        {
            CallEvent?.Invoke(this, new CallEventArgs(CallerNumber, callingNumber));
        }
        public void AnswerToCall(string callingNumber, CallState callState)
        {
            AnswerToCallEvent?.Invoke(this, new AnswerEventArgs(CallerNumber, callingNumber, callState));
        }
        public void EndCall(string callingNumber)
        {
            EndCallEvent?.Invoke(this, new EndEventArgs(CallerNumber, callingNumber));
        }

        public void RequestCallData(Comparison<CallData> orderBy)
        {
            RequestCallDataEvent?.Invoke(this, new GetCallDataEventArgs(CallerNumber, orderBy));
        }

        public void IncomingCallEvent(object sender, CallEventArgs e)
        {
            char key;
            while (true)
            {
                Console.WriteLine("{0} calling to {1}, answer?", e.CallerNumber, e.CallingNumber);
                key = Console.ReadKey().KeyChar;
                switch (key)
                {
                    case '+': { AnswerToCall(e.CallingNumber, CallState.Answer); return; }
                    case '-': { AnswerToCall(e.CallingNumber, CallState.Reject); return; }
                    default: { Console.WriteLine("Wrong input, try again"); break; }
                }
            }
        }
        public void AnswerCall(object sender, AnswerEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine((e.CallState == CallState.Answer) ? $"{e.CallerNumber} answers to {e.CallingNumber}'s call" :
                $"{e.CallerNumber} rejects {e.CallingNumber}'s call");
        }
        public void ShowCallsList(object sender,GetCallDataEventArgs e)
        {
            int count = 0;
            foreach (CallData call in e.Calls)
            {
                count++;
                Console.WriteLine("Call {0}",count);
                Console.WriteLine("Number: {0}; Cost: {1}; Start time: {2}; End time: {3}",call.CallingNumber,call.Cost,call.StartTime,call.EndTime);
            }
        }

    }
}
