using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Exceptions
{
    class NullSignUpException : Exception
    {
        public NullSignUpException(string message) : base(message) { }
    } 
}
