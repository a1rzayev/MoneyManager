using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Helpers
{
    public interface IPayType
    {
        public UInt64 Balance { get; set; }
        public CurrencyEnum Currency { get; set; }
    }
}
