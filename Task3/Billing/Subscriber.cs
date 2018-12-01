using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Subscriber
    {
        public string Name { get; private set; }
        public List<CallData> Calls { get; private set; }
        public int Balance { get; private set; }

        public Subscriber(string name, int balance)
        {
            Name = name;
            Calls = new List<CallData>();
            Balance = balance;
        }

        public CallData GetCallingNumberByCallerNumber(string callerNumber)
        {
            foreach (CallData call in Calls)
            {
                if (call.CallerNumber==callerNumber)
                {
                    return call;
                }
            }
            return null;
        }

        

        public void DepositMoney(int money)
        {
            Balance += money;
        }
        public void PayMoney(int money)
        {
            Balance -= money;
        }
    }
}
