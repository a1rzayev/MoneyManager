using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MoneyManager.Messages;
using MoneyManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel { get => currentViewModel; set => Set(ref currentViewModel, value); }
        public INavigationService NavigationService { get; set; }
        public IMessenger Messenger { get; set; }
        public MainViewModel(IMessenger messenger, INavigationService navigationService)
        {
            NavigationService = navigationService;

            messenger.Register<NavigationMessage>(this, message =>
            {
                var viewModel = App.container.GetInstance(message.ViewModelType) as ViewModelBase;
                CurrentViewModel = viewModel;
            });

        }
    }
}
