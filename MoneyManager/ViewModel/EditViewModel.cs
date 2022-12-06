using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MoneyManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyManager.ViewModel
{
    public class EditViewModel : ViewModelBase
    {
        public IFileService FileService { get; set; }
        public IPayService PayService { get; set; }
        public IMessenger Messenger { get; set; }
        public INavigationService NavigationService { get; set; }


        private Int32 selectedIndex;
        public Int32 SelectedIndex { get => selectedIndex; set => Set(ref selectedIndex, value); }


        private Int32 selectedPayTypeIndex;
        public Int32 SelectedPayTypeIndex { get => selectedPayTypeIndex; set => Set(ref selectedPayTypeIndex, value); }



        private List<String> payWays = new List<String>();
        public List<String> PayWays { get => payWays; set => Set(ref payWays, value); }



        private String chosenCategory;
        public String ChosenCategory { get => chosenCategory; set => Set(ref chosenCategory, value); }


        private UInt64 summ;
        public UInt64 Summ { get => summ; set => Set(ref summ, value); }



        private List<String> categories = new List<String>();
        public List<String> Categories { get => categories; set => Set(ref categories, value); }

        private String buttonText;
        public String ButtonText { get => buttonText; set => Set(ref buttonText, value); }


        public EditViewModel(INavigationService navigationService, IMessenger messenger, IPayService payService, IFileService fileService)
        {
            NavigationService = navigationService;
            Messenger = messenger;
            PayService = payService;
            FileService = fileService;
        }
        public RelayCommand BackToHome
        {
            get => new RelayCommand(() =>
            {
                //App.container.GetInstance<HomeViewModel>().EditBalanceView.Close(); 
                FileService.SaveCurrentUser();
                FileService.SaveCurrentUserById();
                NavigationService.SendInfoToHomeView();
                NavigationService.NavigateTo<HomeViewModel>();
                //App.windowContainer[0].Close();
                //App.windowContainer.Clear();
                //NavigationService.NavigateTo<HomeViewModel>();  
            });
        }
        public RelayCommand Edit
        {
            get => new RelayCommand(() =>
            {
                try
                {
                    if (ButtonText == "+")
                    {
                        DateTime dateTime = DateTime.Now;
                        PayService.Earn(App.currentUser, ChosenCategory, Summ, SelectedIndex);
                        FileService.SaveCurrentUser();
                        FileService.SaveCurrentUserById();
                        Summ = 0;
                        SelectedIndex = 0;
                        SelectedPayTypeIndex = 0;
                    }
                    else if (ButtonText == "-")
                    {
                        DateTime dateTime = DateTime.Now;
                        PayService.Spend(App.currentUser, ChosenCategory, Summ, SelectedIndex);
                        FileService.SaveCurrentUser();
                        FileService.SaveCurrentUserById();
                        Summ = 0;
                        SelectedIndex = 0;
                        SelectedPayTypeIndex = 0;
                    }
                    FileService.SaveCurrentUser();
                    FileService.SaveCurrentUserById();
                    NavigationService.SendInfoToHomeView();
                    NavigationService.NavigateTo<HomeViewModel>();
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            });
        }
    } 
}
