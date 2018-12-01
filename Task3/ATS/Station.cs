using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task3
{
    public class Station
    {
        public List<Contract> Contracts { get; private set; }
        private Dictionary<string,Port> Ports { get; set; }

        public Station()
        {
            Contracts = new List<Contract>();
            Ports = new Dictionary<string, Port>();
        }

        public string GenerateSubscriberNumber()
        {
            string number = "";
            Random rnd = new Random();
            while (true)
            {
                for (int i = 0; i < 6; i++)
                {
                    number += rnd.Next() % 10;
                }
                if (Ports.Keys.FirstOrDefault(x => x == number) == null)
                {
                    Thread.Sleep(100);
                    return number;
                }
                number = "";
            }
        }
        public Contract SignContract(Subscriber subscriber, TariffPlan tariffPlan)
        {
            return new Contract(subscriber, GenerateSubscriberNumber(), new Tariff(tariffPlan));
        }

        public Terminal CreateTerminal(Contract contract)
        {
            string number;
            Port port = new Port();
            port.CallEvent += Port_CallEvent;
            port.AnswerToCallEvent += Port_AnswerToCallEvent;
            port.EndCallEvent += Port_EndCallEvent;

            port.RequestCallDataEvent += Port_RequestCallDataEvent;
            number = contract.Number;
            Contracts.Add(contract);
            return new Terminal(contract.Number, port);
        }

        private void Port_RequestCallDataEvent(object sender, GetCallDataEventArgs e)
        {
            // Contracts.Find(x => x.Number==e.CallerNumber).Subscriber.Calls.Sort(e.OrderBy);
            e.Calls = Contracts.Find(x => x.Number == e.CallerNumber).Subscriber.Calls;
            (sender as Port).Port_ReturnCallDataEvent(sender,e);
        }

        private void Port_EndCallEvent(object sender, EndEventArgs e)
        {
            
            CallData callerCall = null, callingCall = null;
            Contract callerContract, callingContract;

            callerContract = Contracts.Find(x => x.Number == e.CallerNumber);
            callingContract = Contracts.Find(x => x.Number == e.CallingNumber);
            callerCall = callerContract.Subscriber.Calls.FindLast(x => x.CallerNumber == e.CallerNumber);
            callingCall = callingContract.Subscriber.Calls.FindLast(x => x.CallerNumber == e.CallingNumber);
            if ((callerCall != null)&& (callingCall != null))
            {
                callerCall = new CallData(callerCall.CallerNumber, callerCall.CallingNumber,
                    ((DateTime.Now - callerCall.StartTime).Minutes + 1) * callerContract.Tariff.CostPerMinute
                    , callerCall.StartTime, DateTime.Now);
                callingCall = new CallData(callingCall.CallerNumber, callingCall.CallingNumber,
                    ((DateTime.Now - callingCall.StartTime).Minutes + 1) * callingContract.Tariff.CostPerMinute
                    , callingCall.StartTime, DateTime.Now);

                Contracts[Contracts.FindIndex(x => x.Number == e.CallerNumber)].Subscriber.Calls
                [Contracts[Contracts.FindIndex(x => x.Number == e.CallerNumber)].Subscriber.Calls.FindLastIndex(x => x.CallerNumber == e.CallerNumber)]
                = callerCall;
                Contracts[Contracts.FindIndex(x => x.Number == e.CallingNumber)].Subscriber.Calls
                [Contracts[Contracts.FindIndex(x => x.Number == e.CallingNumber)].Subscriber.Calls.FindLastIndex(x => x.CallerNumber == e.CallingNumber)]
                = callingCall;
                callerContract.Subscriber.PayMoney(callerCall.Cost);
                callingContract.Subscriber.PayMoney(callingCall.Cost);
            }
        }

        private void Port_AnswerToCallEvent(object sender, AnswerEventArgs e)
        {
            var port = Ports[e.CallerNumber];
            if (port != null)
            {
                if (e.CallState==CallState.Answer)
                {
                    Contracts.Find(x => x.Number == e.CallerNumber).Subscriber.Calls.Add(new CallData(e.CallerNumber, e.CallingNumber));
                    Contracts.Find(x => x.Number == e.CallingNumber).Subscriber.Calls.Add(new CallData(e.CallingNumber, e.CallerNumber));
                    port.AnswerCall(e.CallerNumber, e.CallingNumber, e.CallState);
                }
                else
                {
                    port.AnswerCall(e.CallerNumber, e.CallingNumber, e.CallState);
                }
                
            }
        }

        private void Port_CallEvent(object sender, CallEventArgs e)
        {
            if (!Ports.ContainsKey(e.CallerNumber))
            {
                Ports.Add(e.CallerNumber, (Port)sender);
            }
            var port = Ports[e.CallerNumber];
            if (port!=null)
            {
                port.IncomingCall(e.CallerNumber,e.CallingNumber);
            }
        }
    }
}
