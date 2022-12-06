using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using MoneyManager.Helpers;
using MoneyManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MoneyManager.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private String name;
        public String Name { get => name; set => Set(ref name, value); }

        private String surname;
        public String Surname { get => surname; set => Set(ref surname, value); }

        private String mail;
        public String Mail { get => mail; set => Set(ref mail, value); }

        private String password;
        public String Password { get => password; set => Set(ref password, value); }

        private String imageButtonText;
        public String ImageButtonText { get => imageButtonText; set => Set(ref imageButtonText, value); }

        private DateTime birthDate;
        public DateTime BirthDate { get => birthDate; set => Set(ref birthDate, value); }

        private CurrencyEnum defaultCurrency;
        public CurrencyEnum DefaultCurrency { get => defaultCurrency; set => Set(ref defaultCurrency, value); }

        private Gender gender;
        public Gender Gender { get => gender; set => Set(ref gender, value); }

        private String profilePhoto;
        public String ProfilePhoto { get => profilePhoto; set => Set(ref profilePhoto, value); }

        private String sstartCash;
        public String SstartCash { get => sstartCash; set => Set(ref sstartCash, value); }

        private UInt64 startCash;
        public UInt64 StartCash { get => startCash; set => Set(ref startCash, value); }

        public IMessenger Messenger { get; set; }
        public INavigationService NavigationService { get; set; }
        public ILogInService LogInService { get; set; }
        public IFileService FileService { get; set; }
        public ISignUpService SignUpService { get; set; } 
        public SettingsViewModel(INavigationService navigationService, IMessenger messenger, ILogInService logInService, IFileService fileService, ISignUpService signUpService)
        {
            NavigationService = navigationService;
            Messenger = messenger;
            LogInService = logInService;
            FileService = fileService;  
            SignUpService = signUpService;
        }
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
        public RelayCommand Save
        {
            get => new RelayCommand(() =>
            {
                try
                { 
                    SignUpService.EditInfo(Name, Surname, Mail, BirthDate, ProfilePhoto, DefaultCurrency, Gender, Password); 
                    FileService.SaveCurrentUser();
                    FileService.SaveCurrentUserById();
                    NavigationService.SendInfoToHomeView(); 
                    NavigationService.NavigateTo<HomeViewModel>();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            });
        }
        public RelayCommand SelectPhoto
        {
            get => new RelayCommand(() =>
            {
                var imageDialog = new OpenFileDialog();
                imageDialog.Filter = "(*.jpg)|*.jpg|(*.jpeg)|*.jpeg|(*.jfif)|*.jfif|(*.png)|*.png";
                var result = imageDialog.ShowDialog();
                ProfilePhoto = imageDialog.FileName;
                ImageButtonText = ProfilePhoto;
            });
        }
    }
}
