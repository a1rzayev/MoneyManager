using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MoneyManager.Exceptions;
using MoneyManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MoneyManager.View;
using System.Windows.Controls;

namespace MoneyManager.ViewModel
{
    public class LogInViewModel : ViewModelBase
    {
        private String mail;
        public String Mail { get => mail; set => Set(ref mail, value); }
        private String password;
        public String Password { get => password; set => Set(ref password, value); }
        public IMessenger Messenger { get; set; }
        public INavigationService NavigationService { get; set; }
        public ILogInService LogInService { get; set; }
        public IFileService FileService { get; set; }

        public void Clear()
        {
            Mail = "";
            Password = "";
        }
        public LogInViewModel(INavigationService navigationService, IMessenger messenger, ILogInService logInService, IFileService fileService)
        {
            NavigationService = navigationService;
            Messenger = messenger;
            LogInService = logInService;
            FileService = fileService; 
        }
        public RelayCommand GotoSignUp
        {
            get => new RelayCommand(() =>
            {
                //Clear();
                NavigationService.NavigateTo<SignUpViewModel>();
            });
        }
        //public RelayCommand LogIn
        //{
        //    get => new RelayCommand(() =>
        //    {
        //        try
        //        {
        //            LogInService.LogIn(Mail, Password);
        //            NavigationService.SendInfoToHomeView();
        //            //Clear();
        //            NavigationService.NavigateTo<HomeViewModel>();
        //        }
        //        catch (WrongLogInException wlex)
        //        {
        //            MessageBox.Show($"Error: {wlex.Message}");
        //        }
        //    });
        //} 
        public RelayCommand<PasswordBox> LogIn
        {
            get => new RelayCommand<PasswordBox>(
                param =>
            {
                try
                {
                    var a = param as PasswordBox;
                    LogInService.LogIn(Mail, a.Password);
                    NavigationService.SendInfoToHomeView();
                    //Clear();
                    NavigationService.NavigateTo<HomeViewModel>();
                }
                catch (WrongLogInException wlex)
                {
                    MessageBox.Show($"Error: {wlex.Message}");
                }
            });
        }
    }
}
