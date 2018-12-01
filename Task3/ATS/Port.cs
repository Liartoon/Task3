using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Port
    {
        public PortState PortState { get; set; }

        public event EventHandler<CallEventArgs> CallEvent;
        public event EventHandler<CallEventArgs> IncomingCallEvent;

        public event EventHandler<AnswerEventArgs> AnswerToCallEvent;
        public event EventHandler<AnswerEventArgs> AnswerCallEvent;

        public event EventHandler<EndEventArgs> EndCallEvent;

        public event EventHandler<GetCallDataEventArgs> RequestCallDataEvent;
        public event EventHandler<GetCallDataEventArgs> ReturnCallDataEvent;

        public Port()
        {
            PortState = PortState.Disconnected;
        }

        public bool Connect(Terminal terminal)
        {
            if (PortState == PortState.Disconnected)
            {
                PortState = PortState.Connected;
                terminal.CallEvent += Call;
                terminal.AnswerToCallEvent += AnswerToCall;
                terminal.EndCallEvent += EndCall;

                terminal.RequestCallDataEvent += Terminal_RequestCallDataEvent;
                return true;
            }
            return false;
        }

        public void Port_ReturnCallDataEvent(object sender, GetCallDataEventArgs e)
        {
            ReturnCallDataEvent(this,e);
        }

        private void Terminal_RequestCallDataEvent(object sender, GetCallDataEventArgs e)
        {
            RequestCallDataEvent?.Invoke(this,e);
        }

        public bool Disconnect(Terminal terminal)
        {
            if (PortState == PortState.Connected)
            {
                PortState = PortState.Disconnected;
                terminal.CallEvent -= Call;
                terminal.AnswerToCallEvent -= AnswerToCall;
                terminal.EndCallEvent -= EndCall;

                terminal.RequestCallDataEvent += Terminal_RequestCallDataEvent;
                return true;
            }
            return false;
        }

        public void Call(object sender, CallEventArgs e)
        {
            CallEvent?.Invoke(this,e);
        }
        public void IncomingCall(string callerNumber,string callingNumber)
        {
            IncomingCallEvent?.Invoke(this,new CallEventArgs(callerNumber, callingNumber));
        }

        public void AnswerToCall(object sender,AnswerEventArgs e)
        {
            AnswerToCallEvent?.Invoke(this,e);
        }
        public void AnswerCall(string callerNumber, string callingNumber,CallState callState)
        {
            AnswerCallEvent?.Invoke(this,new AnswerEventArgs(callerNumber, callingNumber, callState));
        }

        public void EndCall(object sender, EndEventArgs e)
        {
            EndCallEvent?.Invoke(this,e);
        }
    }
}
