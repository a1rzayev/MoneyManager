using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyManager.Model;
using MoneyManager.Exceptions;
using MoneyManager.Classes;
using MoneyManager.Helpers;
using MoneyManager.ViewModel;

namespace MoneyManager.Services
{
    public class PayService : IPayService
    {
        public void Spend(UserModel user, String name, UInt64 summ, Int32 Selected)
        { 
            FileService fileService = new FileService();
            DateTime dateTime = DateTime.Now;
            if (name == null) throw new Exception("Category is not selected"); 
            else if (Selected == 0 && user.Cash.Balance >= summ)
            { 
                user.Cash.Balance -= summ;
                user.SpendCategories[name] += summ;
                App.history.Add($"{name}  [Cash]  -{summ}, {dateTime}");
                App.container.GetInstance<HomeViewModel>().History = App.history;
                //App.container.GetInstance<HomeViewModel>().SpendCategoriesStatistics[name] += summ;
                fileService.SaveHistory(@"C:\Money Manager\Common\History.json");
                fileService.SaveHistory(@$"C:\Money Manager\Users\{user.Id}\History.json");
            }
            else if(Selected > 0 && user.PayWays[Selected - 1].Balance >= summ)
            { 
                user.PayWays[Selected - 1].Balance -= summ;
                user.SpendCategories[name] += summ;
                App.history.Add($"{name}  [({user.PayWays[Selected - 1].Id}) {user.PayWays[Selected - 1].Company}]  -{summ}, {dateTime}");
                App.container.GetInstance<HomeViewModel>().History = App.history;
                //App.container.GetInstance<HomeViewModel>().SpendCategoriesStatistics[name] += summ;
                fileService.SaveHistory(@"C:\Money Manager\Common\History.json");
                fileService.SaveHistory(@$"C:\Money Manager\Users\{user.Id}\History.json");
            }
            else throw new NotEnoughMoneyException("Not enough money");
        }
        public void Earn(UserModel user, String name, UInt64 summ, Int32 Selected)
        {
            FileService fileService = new FileService();
            DateTime dateTime = DateTime.Now; 
            if (name == null) throw new Exception("Category is not selected");
            else if (Selected == 0) {
                user.Cash.Balance += summ;
                user.IncomeCategories[name] += summ;
                App.history.Add($"{name}  [Cash]  +{summ}, {dateTime}");
                App.container.GetInstance<HomeViewModel>().History = App.history;
                //App.container.GetInstance<HomeViewModel>().IncomeCategoriesStatistics[name] += summ;
                fileService.SaveHistory(@"C:\Money Manager\Common\History.json");
                fileService.SaveHistory(@$"C:\Money Manager\Users\{user.Id}\History.json");

            }
            else { 
                user.PayWays[Selected - 1].Balance += summ;
                user.IncomeCategories[name] += summ;
                App.history.Add($"{name}  [({user.PayWays[Selected - 1].Id}) {user.PayWays[Selected - 1].Company}]  +{summ}, {dateTime}");
                App.container.GetInstance<HomeViewModel>().History = App.history;
                //App.container.GetInstance<HomeViewModel>().IncomeCategoriesStatistics[name] += summ;
                fileService.SaveHistory(@"C:\Money Manager\Common\History.json");
                fileService.SaveHistory(@$"C:\Money Manager\Users\{user.Id}\History.json");
            } 
        }
        public void Add(UserModel user, Card card)
        {
            IFileService fileService = new FileService();
            App.currentUser.PayWays.Add(card); 
            fileService.SaveCurrentUserById();
            fileService.SaveCurrentUser();
            ++App.currentCardId;
            fileService.SaveId(App.currentCardId, @"C:\Money Manager\Common\Current Card Id.txt");
        }
        public void Add(UserModel user, Tuple<String, UInt64> tuple, String selectedType)
        { 
            IFileService fileService = new FileService();
            if (selectedType == "Spend") App.currentUser.SpendCategories.Add(tuple.Item1, tuple.Item2); 
            else if (selectedType == "Income") App.currentUser.IncomeCategories.Add(tuple.Item1, tuple.Item2); 
            fileService.SaveCurrentUserById();
            fileService.SaveCurrentUser();
        }
        public void Remove(UserModel user, String key, String selectedType)
        {
            IFileService fileService = new FileService();
            if (selectedType == "Spend") App.currentUser.SpendCategories.Remove(key); 
            else if (selectedType == "Income") App.currentUser.IncomeCategories.Remove(key);
            fileService.SaveCurrentUserById();
            fileService.SaveCurrentUser();
        }
    }
}
