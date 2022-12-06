using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MoneyManager.Classes;
using MoneyManager.Messages;
using MoneyManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Services
{
    public class NavigationService : INavigationService
    {
        public readonly IMessenger messenger;
        public NavigationService(IMessenger _messenger) { messenger = _messenger; }
        public void NavigateTo<T>() where T : ViewModelBase
        {
            messenger.Send(new NavigationMessage() { ViewModelType = typeof(T) }); 
        }
        public void SendInfoToHomeView()
        { 
            UInt64 a = App.currentUser.Cash.Balance;
            List<String> c = new List<String>();
            List<String> s = new List<String>();
            List<String> u = new List<String>();
            App.container.GetInstance<HomeViewModel>().PhotoSource = App.currentUser.ProfilePhoto;  
            c.Add($"Cash  [{App.currentUser.Cash.Balance} {App.currentUser.Cash.Currency.ToString()}]");
            foreach (var item in App.currentUser.PayWays)
            {
                c.Add($"({item.Id}) {item.Company}  [{item.Balance} {item.Currency.ToString()}]");
                a += item.Balance;
            }
            foreach (var item in App.currentUser.IncomeCategories) u.Add($"{item.Key}    [{item.Value}]");
            foreach (var item in App.currentUser.SpendCategories) s.Add($"{item.Key}    [{item.Value}]");
            App.container.GetInstance<HomeViewModel>().Balance = a;
            App.container.GetInstance<HomeViewModel>().PayWays = c;
            App.container.GetInstance<HomeViewModel>().History = App.history;
            App.container.GetInstance<HomeViewModel>().IncomeCategoriesStatistics = u;
            App.container.GetInstance<HomeViewModel>().SpendCategoriesStatistics = s;

        }
        public void SendInfoToRemoveView()
        {
            List<String> a = new List<String>();
            List<String> b = new List<String>();
            foreach (var elem in App.currentUser.IncomeCategories)
            {
                a.Add(elem.Key);
            }
            foreach (var elem in App.currentUser.SpendCategories)
            {
                b.Add(elem.Key);
            }
            App.container.GetInstance<RemoveViewModel>().IncomeCategories = a;
            App.container.GetInstance<RemoveViewModel>().SpendCategories = b; 
        }
        public void SendInfoToSettingsView()
        {
            App.container.GetInstance<SettingsViewModel>().Mail = App.currentUser.Mail;
            App.container.GetInstance<SettingsViewModel>().Password = App.currentUser.Password;
            App.container.GetInstance<SettingsViewModel>().Name = App.currentUser.Name;
            App.container.GetInstance<SettingsViewModel>().Surname = App.currentUser.Surname;
            App.container.GetInstance<SettingsViewModel>().BirthDate = App.currentUser.BirthDate;
            App.container.GetInstance<SettingsViewModel>().ProfilePhoto = App.currentUser.ProfilePhoto;
            App.container.GetInstance<SettingsViewModel>().DefaultCurrency = App.currentUser.DefaultCurrency;
            App.container.GetInstance<SettingsViewModel>().Gender = App.currentUser.Gender; 
        }
        public void SendInfoToEditView(String CategoryType)
        {
            List<String> c = new List<String>(); 
            if(CategoryType == "Spend")
            {
                List<String> a = new List<String>();
                foreach (var item in App.currentUser.SpendCategories)
                {
                    a.Add(item.Key);
                } 
                App.container.GetInstance<EditViewModel>().Categories = a;

                App.container.GetInstance<EditViewModel>().ButtonText = "-";

                c.Add($"Cash  [{App.currentUser.Cash.Balance} {App.currentUser.Cash.Currency.ToString()}]");
                foreach (var item in App.currentUser.PayWays) c.Add($"({item.Id}) {item.Company}  [{item.Balance} {item.Currency.ToString()}]");  
                App.container.GetInstance<EditViewModel>().PayWays = c;

            }
            else if(CategoryType == "Income")
            { 
                List<String> b = new List<String>();
                foreach (var item in App.currentUser.IncomeCategories)
                {
                    b.Add(item.Key);
                }
                App.container.GetInstance<EditViewModel>().Categories = b;

                App.container.GetInstance<EditViewModel>().ButtonText = "+";

                c.Add($"Cash  [{App.currentUser.Cash.Balance} {App.currentUser.Cash.Currency.ToString()}]");
                foreach (var item in App.currentUser.PayWays) c.Add($"({item.Id}) {item.Company}  [{item.Balance} {item.Currency.ToString()}]");  
                App.container.GetInstance<EditViewModel>().PayWays = c;
            }
            else
            {
                throw new Exception("Unhandled Error!");
            }
        }
    }
}
