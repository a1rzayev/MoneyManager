using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Services
{

    public interface INavigationService
    {
        public void NavigateTo<T>() where T : ViewModelBase;
        public void SendInfoToHomeView();
        public void SendInfoToRemoveView();
        public void SendInfoToSettingsView();
        public void SendInfoToEditView(String CategoryType);
    }
}
