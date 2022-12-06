using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MoneyManager.Classes;
using MoneyManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.ViewModel
{
    public class RemoveViewModel : ViewModelBase
    { 

        private List<String> spendCategories = new List<string>();
        public List<String> SpendCategories { get => spendCategories; set => Set(ref spendCategories, value); }
        private List<String> incomeCategories = new List<string>();
        public List<String> IncomeCategories { get => incomeCategories; set => Set(ref incomeCategories, value); }
        private List<String> payWays = new List<String>();
        public List<String> PayWays { get => payWays; set => Set(ref payWays, value); }
        private String selectedIncomeCategory;
        public String SelectedIncomeCategory { get => selectedIncomeCategory; set => Set(ref selectedIncomeCategory, value); }
        private String selectedSpendCategory;
        public String SelectedSpendCategory { get => selectedSpendCategory; set => Set(ref selectedSpendCategory, value); }
        public INavigationService NavigationService { get; set; }
        public IMessenger Messenger { get; set; }
        public IPayService PayService { get; set; }
        public IFileService FileService { get; set; }
        public RelayCommand BackToHome
        {
            get => new RelayCommand(() =>
            {
                FileService.SaveCurrentUser();
                FileService.SaveCurrentUserById();
                NavigationService.SendInfoToHomeView();
                NavigationService.NavigateTo<HomeViewModel>();
            });
        }
        public RelayCommand RemoveIncomeCategory
        {
            get => new RelayCommand(() =>
            {
                PayService.Remove(App.currentUser, selectedIncomeCategory, "Income");
                FileService.SaveCurrentUser();
                FileService.SaveCurrentUserById();
                NavigationService.SendInfoToHomeView();
                NavigationService.NavigateTo<HomeViewModel>();
            });
        }
        public RelayCommand RemoveSpendCategory
        {
            get => new RelayCommand(() =>
            {
                PayService.Remove(App.currentUser, selectedSpendCategory, "Spend");
                FileService.SaveCurrentUser();
                FileService.SaveCurrentUserById();
                NavigationService.SendInfoToHomeView();
                NavigationService.NavigateTo<HomeViewModel>();
            });
        } 
        public RemoveViewModel (INavigationService navigationService, IMessenger messenger, IPayService payService, IFileService fileService) {
            NavigationService = navigationService;
            Messenger = messenger;
            PayService = payService;
            FileService = fileService;
        } 
    }
}
