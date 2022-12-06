using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Services
{
    public interface ILogInService
    {
        public static UInt64 SearchId(String Mail, String Password) { return 0; }
        public void LogIn(String Mail, String Password);
        public static Boolean OnStartUp() { return false; }
    }
}
