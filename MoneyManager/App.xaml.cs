using MoneyManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MoneyManager.Services;
using GalaSoft.MvvmLight;
using MoneyManager.View;
using GalaSoft.MvvmLight.Messaging;
using SimpleInjector;
using MoneyManager.Model; 

namespace MoneyManager
{ 
    public partial class App : Application
    {
        public static UInt64 currentId { get; set; } = 1;
        public static UInt64 currentCardId { get; set; } = 0;
        public static List<UserModel> usersList { get; set; } = new List<UserModel>();
        public static UserModel? currentUser { get; set; } = new UserModel();
        public static List<String>? history { get; set; } = new List<String>();
        public static Container container { get; private set; } = new Container();
        public static List<Window> windowContainer { get; set; } = new List<Window>();
        public static Container userContainer { get; private set; } = new Container();
        protected override void OnStartup(StartupEventArgs e)
        {
            Register();
            FileService.OnStartUp();
            if (LogInService.OnStartUp())
            {
                IMessenger Messenger = new Messenger();
                ILogInService LogInService = new LogInService();
                INavigationService NavigationService = new NavigationService(Messenger);
                base.OnStartup(e);
                LogInService.LogIn(App.currentUser.Mail, App.currentUser.Password);
                NavigationService.SendInfoToHomeView();
                StartMain<HomeViewModel>(); 
            }
            else
            {
                StartMain<LogInViewModel>();
                base.OnStartup(e);
            }
        }
        public void Register()
        {
            container.RegisterSingleton<IFileService, FileService>();
            container.RegisterSingleton<ILogInService, LogInService>();
            container.RegisterSingleton<INavigationService, NavigationService>();
            container.RegisterSingleton<IMessenger, Messenger>();
            container.RegisterSingleton<ISignUpService, SignUpService>();
            container.RegisterSingleton<IPayService, PayService>();
            container.RegisterSingleton<IErrorLogger, ErrorLogger>();

            container.RegisterSingleton<MainViewModel>();
            container.RegisterSingleton<HomeViewModel>();
            container.RegisterSingleton<LogInViewModel>();
            container.RegisterSingleton<SettingsViewModel>();
            container.RegisterSingleton<SignUpViewModel>();
            container.RegisterSingleton<AddViewModel>();
            container.RegisterSingleton<RemoveViewModel>();
            container.RegisterSingleton<EditViewModel>();
             

            container.Verify();
        }
        public void StartMain<T>() where T : ViewModelBase
        {
            var viewModel = container.GetInstance<MainViewModel>();
            viewModel.CurrentViewModel = container.GetInstance<T>();
            Window window = new MainView() { DataContext = viewModel };
            window.ShowDialog();
        }
    }
}