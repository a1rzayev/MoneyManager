using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Exceptions
{
    class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException(string message) : base(message) { }
    }
}
