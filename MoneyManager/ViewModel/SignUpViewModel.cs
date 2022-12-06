using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using MoneyManager.Helpers;
using MoneyManager.Model;
using MoneyManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MoneyManager.Exceptions;
using MoneyManager.View;

namespace MoneyManager.ViewModel
{
    public class SignUpViewModel : ViewModelBase
    {
        private String name;
        public String Name { get => name; set => Set(ref name, value); }

        private String surname;
        public String Surname { get => surname; set => Set(ref surname, value); }

        private String mail;
        public String Mail { get => mail; set => Set(ref mail, value); }

        private String password;
        public String Password { get => password; set => Set(ref password, value); }

        private String imageButtonText = "Browse";
        public String ImageButtonText { get => imageButtonText; set => Set(ref imageButtonText, value); }

        private DateTime birthDate = new DateTime(1980, 1, 1);
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

        public  void Clear() {
            Name = "";
            Surname = "";
            Mail = "";
            Password = "";
            ProfilePhoto = "";
        }


        public IFileService FileService { get; set; }
        public IMessenger Messenger { get; set; }
        public INavigationService NavigationService { get; set; }
        public ISignUpService SignUpService { get; set; }

        public SignUpViewModel(INavigationService navigationService, IMessenger messenger, ISignUpService signUpService, IFileService fileService)
        {
            NavigationService = navigationService;
            Messenger = messenger;
            SignUpService = signUpService;
            FileService = fileService;

        }
        public RelayCommand GotoLogIn
        {
            get => new RelayCommand(() =>
            {
                //Clear();
                NavigationService.NavigateTo<LogInViewModel>();
            });
        }
        //public RelayCommand SignUp
        //{
        //    get => new RelayCommand(() =>
        //    {
        //        try
        //        {
        //            SignUpService.SignUp(Name, Surname, Mail, BirthDate, ProfilePhoto, DefaultCurrency, SstartCash, Gender, Password);
        //            NavigationService.SendInfoToHomeView();
        //            //Clear();
        //            NavigationService.NavigateTo<HomeViewModel>();

        //        } 
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Error: {ex.Message}");
        //        }
        //    });
        //}
        public RelayCommand<PasswordBox> SignUp
        {
            get => new RelayCommand<PasswordBox>(
                param =>
                {
                    try
                    {
                        var a = param as PasswordBox;
                        SignUpService.SignUp(Name, Surname, Mail, BirthDate, ProfilePhoto, DefaultCurrency, SstartCash, Gender, a.Password);
                        NavigationService.SendInfoToHomeView();
                        //Clear();
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
