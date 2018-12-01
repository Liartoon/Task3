using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Tariff
    {
        public int SubscriptionFee { get; private set; }
        public int CostPerMinute { get; private set; }
        public int FreeMinutes { get; private set; }
        public TariffPlan TariffPlan { get; private set; }

        public Tariff(TariffPlan tariffPlan)
        {
            switch (tariffPlan)
            {
                case TariffPlan.Standart:
                    {
                        SubscriptionFee = 0;
                        CostPerMinute = 3;
                        FreeMinutes = 0;
                        break;
                    }
                case TariffPlan.Premium:
                    {
                        SubscriptionFee = 10;
                        CostPerMinute = 2;
                        FreeMinutes = 50;
                        break;
                    }
                case TariffPlan.Gold:
                    {
                        SubscriptionFee = 30;
                        CostPerMinute = 1;
                        FreeMinutes = 200;
                        break;
                    }
            }
        }
    }
}
