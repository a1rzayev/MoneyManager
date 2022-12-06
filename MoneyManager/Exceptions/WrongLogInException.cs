using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Exceptions
{
    class WrongLogInException : Exception
    {
        public WrongLogInException(string message) : base(message) { }
    }
}
