using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyManager.Model;
using MoneyManager.Classes;
using MoneyManager.Helpers;

namespace MoneyManager.Services
{
    public interface IPayService
    {
        public void Spend(UserModel user, String name, UInt64 summ, Int32 Selected);
        public void Earn(UserModel user, String name, UInt64 summ, Int32 Selected);
        public void Add(UserModel user, Card card);
        public void Add(UserModel user, Tuple<String, UInt64> tuple, String selectedType);
        public void Remove(UserModel user, String key, String selectedType);
    }
}