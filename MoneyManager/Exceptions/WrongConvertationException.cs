using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Exceptions
{
    public class WrongConvertationException : Exception
    {
        public WrongConvertationException(string message) : base(message) { }
    }
}
