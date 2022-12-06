using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MoneyManager.Helpers;

namespace MoneyManager.Services
{
    public interface ISignUpService
    {
        public static bool AreParamsCorrect(String Name, String Surname, String Mail, DateTime BirthDate, String ProfilePhoto, CurrencyEnum DefaultCurrency, Gender Gender, String Password) { return false; }
        public static bool IsPasswordCorrect(String Password) { return false; }
        public void SignUp(String Name, String Surname, String Mail, DateTime BirthDate, String ProfilePhoto, CurrencyEnum DefaultCurrency, String StartCash, Gender Gender, String Password);
        public void EditInfo(String Name, String Surname, String Mail, DateTime BirthDate, String ProfilePhoto, CurrencyEnum DefaultCurrency, Gender Gender, String Password);
    }
}
