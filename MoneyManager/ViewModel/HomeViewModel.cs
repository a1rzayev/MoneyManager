using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MoneyManager.Helpers;
using MoneyManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MoneyManager.Exceptions;
using System.Windows;
using System.Collections.ObjectModel;
using MoneyManager.Classes;
using MoneyManager.View;

namespace MoneyManager.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public IFileService FileService { get; set; }
        public IPayService PayService { get; set; }
        public IMessenger Messenger { get; set; }
        public INavigationService NavigationService { get; set; }


        //private Int32 selectedIncomeIndex;
        //public Int32 SelectedIncomeIndex { get => selectedIncomeIndex; set => Set(ref selectedIncomeIndex, value); }
        //private Int32 selectedSpendIndex;
        //public Int32 SelectedSpendIndex { get => selectedSpendIndex; set => Set(ref selectedSpendIndex, value); }

        private UInt64 balance;
        public UInt64 Balance { get => balance; set => Set(ref balance, value); }

        private List<String> payWays = new List<String>();
        public List<String> PayWays { get => payWays; set => Set(ref payWays, value); }



        private String photoSource;
        public String PhotoSource { get => photoSource; set => Set(ref photoSource, value); }



        //private String chosenSpendCategory;
        //public String ChosenSpendCategory { get => chosenSpendCategory; set => Set(ref chosenSpendCategory, value); }

        //private String chosenIncomeCategory;
        //public String ChosenIncomeCategory { get => chosenIncomeCategory; set => Set(ref chosenIncomeCategory, value); }


        //private UInt64 spendSumm;
        //public UInt64 SpendSumm { get => spendSumm; set => Set(ref spendSumm, value); }
        //private UInt64 incomeSumm;
        //public UInt64 IncomeSumm { get => incomeSumm; set => Set(ref incomeSumm, value); }


        //private List<String> spendCategories = new List<String>();
        //public List<String> SpendCategories { get => spendCategories; set => Set(ref spendCategories, value); }
        //private List<String> incomeCategories = new List<String>();
        //public List<String> IncomeCategories { get => incomeCategories; set => Set(ref incomeCategories, value); }


        private List<String> history = new List<String>();
        public List<String> History { get => history; set => Set(ref history, value); } 


        private List<String> spendCategoriesStatistics = new List<String>();
        public List<String> SpendCategoriesStatistics { get => spendCategoriesStatistics; set => Set(ref spendCategoriesStatistics, value); }
        private List<String> incomeCategoriesStatistics = new List<String>();
        public List<String> IncomeCategoriesStatistics { get => incomeCategoriesStatistics; set => Set(ref incomeCategoriesStatistics, value); } 



        //private IPayType selectedIncomePayType;
        //public IPayType SelectedIncomePayType { get => selectedIncomePayType; set => Set(ref selectedIncomePayType, value); }

        //private IPayType selectedSpendPayType;
        //public IPayType SelectedSpendPayType { get => selectedSpendPayType; set => Set(ref selectedSpendPayType, value); }


        //private Int32 selectedIncomePayTypeIndex;
        //public Int32 SelectedIncomePayTypeIndex { get => selectedIncomePayTypeIndex; set => Set(ref selectedIncomePayTypeIndex, value); }
        //private Int32 selectedSpendPayTypeIndex;
        //public Int32 SelectedSpendPayTypeIndex { get => selectedSpendPayTypeIndex; set => Set(ref selectedSpendPayTypeIndex, value); }

        //public void Clear()
        //{ 
        //    SpendSumm = 0; 
        //    IncomeSumm = 0;
        //    PayWays.Clear();
        //    IncomeCategories.Clear();
        //    //SpendCategories.Clear();
        //    //History.Clear();
        //    IncomeCategoriesStatistics.Clear();
        //    SpendCategoriesStatistics.Clear();
        //}

        public HomeViewModel(INavigationService navigationService, IMessenger messenger, IPayService payService, IFileService fileService)
        {
            NavigationService = navigationService;
            Messenger = messenger;
            PayService = payService;
            FileService = fileService; 

            //PayWays = App.currentUser.PayWays;
            //PhotoSource = App.currentUser.ProfilePhoto;
        }
        //public RelayCommand Earn
        //{
        //    get => new RelayCommand(() =>
        //    {
        //        try
        //        {
        //            DateTime dateTime = DateTime.Now;
        //            PayService.Earn(App.currentUser, ChosenIncomeCategory, IncomeSumm, SelectedIncomeIndex); 
        //            FileService.SaveCurrentUser();
        //            FileService.SaveCurrentUserById();
        //            SpendSumm = 0;
        //            SelectedSpendIndex = 0;
        //            SelectedSpendPayTypeIndex = 0;
        //        }
        //        catch (Exception nemex)
        //        {
        //            MessageBox.Show($"Error: {nemex.Message}");
        //        }
        //    });
        //}
        public RelayCommand Earn
        {
            get => new RelayCommand(() =>
            {
                try
                { 
                    //EditBalanceView editBalanceView = new EditBalanceView();
                    //App.windowContainer.Add(editBalanceView);
                    //editBalanceView.ShowDialog(); 
                    NavigationService.NavigateTo<EditViewModel>(); 
                    NavigationService.SendInfoToEditView("Income");
                }
                catch (Exception nemex)
                {
                    MessageBox.Show($"Error: {nemex.Message}");
                }
            });
        }
        //public RelayCommand Spend                             
        //{
        //    get => new RelayCommand(() =>
        //    {
        //        try
        //        {
        //            DateTime dateTime = DateTime.Now;
        //            PayService.Spend(App.currentUser, ChosenSpendCategory, SpendSumm, SelectedSpendIndex); 
        //            FileService.SaveCurrentUser();
        //            FileService.SaveCurrentUserById();
        //            IncomeSumm = 0;
        //            SelectedIncomeIndex = 0;
        //            SelectedIncomePayTypeIndex = 0;
        //        }
        //        catch (Exception nemex)
        //        {
        //            MessageBox.Show($"Error: {nemex.Message}");
        //        }
        //    });
        //}
        public RelayCommand Spend
        {
            get => new RelayCommand(() =>
            {
                try
                {
                    //EditBalanceView editBalanceView = new EditBalanceView();
                    //App.windowContainer.Add(editBalanceView);
                    //editBalanceView.ShowDialog(); 
                    NavigationService.NavigateTo<EditViewModel>();
                    NavigationService.SendInfoToEditView("Spend");
                }
                catch (Exception nemex)
                {
                    MessageBox.Show($"Error: {nemex.Message}");
                }
            });
        }
        public RelayCommand Add
        {
            get => new RelayCommand(() =>
            {
                //FileService.SaveCurrentUser();
                //FileService.SaveCurrentUserById();
                //Clear();
                NavigationService.NavigateTo<AddViewModel>(); 
            });
        }
        public RelayCommand Remove
        {
            get => new RelayCommand(() =>
            {
                //FileService.SaveCurrentUser();
                //FileService.SaveCurrentUserById();
                NavigationService.SendInfoToRemoveView();
                //Clear();
                NavigationService.NavigateTo<RemoveViewModel>(); 
            });
        }
        public RelayCommand Exit
        {
            get => new RelayCommand(() =>
            { 
                //Clear();
                App.history = null;
                FileService.SaveCurrentUser();
                FileService.SaveHistory(@"C:\Money Manager\Common\History.json");
                FileService.SaveHistory(@$"C:\Money Manager\Users\{App.currentUser.Id}\History.json");
                App.currentUser = null;
                NavigationService.NavigateTo<LogInViewModel>();
            });
        }
        public RelayCommand GotoSettings
        {
            get => new RelayCommand(() =>
            { 
                NavigationService.SendInfoToSettingsView(); 
                //Clear();
                NavigationService.NavigateTo<SettingsViewModel>();
            });
        }
    }
}
