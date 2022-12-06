using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyManager.Exceptions;

namespace MoneyManager.Services
{
    public class LogInService : ILogInService
    {
        public static UInt64 SearchId(String Mail, String Password)
        {
            foreach(var elem in App.usersList)
            {
                if(Mail == elem.Mail)
                {
                    if (Password == elem.Password) return elem.Id;
                    else return 0;
                } 
            }
            return 0;
        }
        public void LogIn(String Mail, String Password)
        {
            FileService fileService = new FileService();
            UInt64 Id = LogInService.SearchId(Mail, Password);
            if (Id != 0)
            {
                fileService.RefreshCurrentUser(Id); 
                fileService.SaveCurrentUser();
                fileService.SaveCurrentUserById();
                FileService.RefreshHistory(@$"C:\Money Manager\Users\{Id}\History.json");
            } 
            else throw new WrongLogInException("Wrong login or password");
        } 
        public static Boolean OnStartUp() {
            if (App.currentUser == null || String.IsNullOrEmpty(App.currentUser.Mail) || String.IsNullOrEmpty(App.currentUser.Password)) return false;
            else return true; 
        }
    }

}
