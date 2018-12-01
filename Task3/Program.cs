using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Station station = new Station();

            List<Terminal> terminals = new List<Terminal>();
            terminals.Add(station.CreateTerminal(station.SignContract(new Subscriber("John", 500), TariffPlan.Standart)));
            terminals.Add(station.CreateTerminal(station.SignContract(new Subscriber("Mark", 300), TariffPlan.Standart)));
            terminals.Add(station.CreateTerminal(station.SignContract(new Subscriber("Jeam", 800), TariffPlan.Standart)));
            terminals.Add(station.CreateTerminal(station.SignContract(new Subscriber("Mike", 110), TariffPlan.Standart)));
            terminals.Add(station.CreateTerminal(station.SignContract(new Subscriber("Kyle", 340), TariffPlan.Standart)));

            terminals[0].ConnectToATS();
            terminals[1].ConnectToATS();
            terminals[2].ConnectToATS();
            terminals[3].ConnectToATS();
            terminals[4].ConnectToATS();

            Random rnd = new Random();
            int index1=1, index2=1;
            for (int i=0;i<5;i++)
            {
                do
                {
                    index1 = rnd.Next() % 5;
                    index2 = rnd.Next() % 5;
                    Thread.Sleep(100);
                } while (index1 == index2);
                    terminals[index1].Call(terminals[index2].CallerNumber);
                Thread.Sleep(1000);
                terminals[index2].EndCall(terminals[index1].CallerNumber);
            }

            /*terminals[0].Call(terminals[1].CallerNumber);
            Thread.Sleep(1000);
            terminals[1].EndCall(terminals[0].CallerNumber);

            terminals[2].Call(terminals[0].CallerNumber);
            Thread.Sleep(2000);
            terminals[2].EndCall(terminals[0].CallerNumber);

            terminals[1].Call(terminals[2].CallerNumber);
            Thread.Sleep(1000);
            terminals[2].EndCall(terminals[1].CallerNumber);*/

            terminals[2].RequestCallData((x,y) => x.CallingNumber.CompareTo(y.CallingNumber));
            terminals[2].RequestCallData((x, y) => x.Cost.CompareTo(y.Cost));
            terminals[2].RequestCallData((x, y) => ((x.EndTime-x.StartTime).TotalSeconds).CompareTo((y.EndTime-y.StartTime).TotalSeconds));
        }
    }
}
