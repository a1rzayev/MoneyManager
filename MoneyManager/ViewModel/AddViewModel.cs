using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using MoneyManager.Classes;
using MoneyManager.Helpers;
using MoneyManager.Services;
using GalaSoft.MvvmLight.Messaging;
using MoneyManager.Exceptions;
using System.Windows;

namespace MoneyManager.ViewModel
{
    public class AddViewModel : ViewModelBase
    {
        private List<String> categoryTypes = new List<string> { "Spend", "Income" };
        public List<String> CategoryTypes { get => categoryTypes; set => Set(ref categoryTypes, value); }
        private String selectedType;
        public String SelectedType { get => selectedType; set => Set(ref selectedType, value); }
        private String categoryName;
        public String CategoryName { get => categoryName; set => Set(ref categoryName, value); }
        public IPayService PayService { get; set; }
        public IFileService FileService { get; set; }
        public IMessenger Messenger { get; set; }
        public INavigationService NavigationService { get; set; }

        private static Random random = new Random();
        private UInt16 cvv = (UInt16)random.Next(1000);

        private Int32 cardLiveLong;
        public Int32 CardLiveLong { get => cardLiveLong; set => Set(ref cardLiveLong, value); }
        private String sstartBalance;
        public String SstartBalance { get => sstartBalance; set => Set(ref sstartBalance, value); }

        private UInt64 startBalance;
        public UInt64 StartBalance { get => startBalance; set => Set(ref startBalance, value); }

        private CardCompanyEnum company;
        public CardCompanyEnum Company { get => company; set => Set(ref company, value); }
        public Boolean Convertable { get; set; }

        public void Clear()
        {
            CardLiveLong = 1;
        }

        public RelayCommand AddCard
        {
            get => new RelayCommand(() =>
            {
                try
                {
                    Convertable = UInt64.TryParse(SstartBalance, out startBalance);
                    if (Convertable)
                    { 
                        ExpirationDate exp = new ExpirationDate(DateTime.Now.Month, DateTime.Now.Year + CardLiveLong);
                        Card card = new Card(App.currentCardId, exp, cvv, StartBalance, App.currentUser.DefaultCurrency, Company);
                        PayService.Add(App.currentUser, card); 
                        FileService.SaveCurrentUser();
                        FileService.SaveCurrentUserById();
                        NavigationService.SendInfoToHomeView();
                        //Clear();
                        NavigationService.NavigateTo<HomeViewModel>();
                    }
                    else throw new WrongConvertationException("Cannot convert");
                }
                catch(WrongConvertationException wcex)
                {
                    MessageBox.Show($"Error: {wcex.Message}");
                }
            });
        }
        public RelayCommand AddCategory
        {
            get => new RelayCommand(() =>
            {
                Tuple<String, UInt64> tuple = new Tuple<String, UInt64>(CategoryName, 0);
                PayService.Add(App.currentUser, tuple, SelectedType); 
                FileService.SaveCurrentUser();
                FileService.SaveCurrentUserById();
                NavigationService.SendInfoToHomeView();
                //Clear();
                NavigationService.NavigateTo<HomeViewModel>();
            });
        }
        public RelayCommand BackToHome
        {
            get => new RelayCommand(() =>
            {
                FileService.SaveCurrentUser();
                FileService.SaveCurrentUserById();
                NavigationService.SendInfoToHomeView();
                //Clear();
                NavigationService.NavigateTo<HomeViewModel>();
            });
        }
        public AddViewModel(INavigationService navigationService, IMessenger messenger, IFileService fileService, IPayService payService)
        {
            NavigationService = navigationService;
            Messenger = messenger;
            FileService = fileService;
            PayService = payService;


            //PayWays = App.currentUser.PayWays;
            //PhotoSource = App.currentUser.ProfilePhoto;
        }
    }
}
