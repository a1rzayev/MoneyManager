using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyManager.Helpers;

namespace MoneyManager.Classes
{
    public class Cash : IPayType
    {
        public UInt64 Balance { get; set; }
        public CurrencyEnum Currency { get; set; }
        public Cash(UInt64 balance, CurrencyEnum currency)
        {
            Balance = balance;
            Currency = currency;
        }
    }
}
