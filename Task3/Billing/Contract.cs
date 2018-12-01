using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Contract
    {
        public Subscriber Subscriber { get; private set; }
        public string Number { get; private set; }
        public Tariff Tariff { get; set; }

        public Contract(Subscriber subscriber, string number, Tariff tariff)
        {
            Subscriber = subscriber;
            Number = number;
            Tariff = tariff;
        }
    }
}
