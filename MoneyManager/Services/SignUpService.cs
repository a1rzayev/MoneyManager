using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MoneyManager.Helpers;
using MoneyManager.Exceptions;
using MoneyManager.Model;

namespace MoneyManager.Services
{
    public class SignUpService : ISignUpService
    {
        public static bool AreParamsCorrect(String Name, String Surname, String Mail, DateTime BirthDate, String ProfilePhoto, CurrencyEnum DefaultCurrency, Gender Gender, String Password)
        {
            if (String.IsNullOrEmpty(Name) ||
                String.IsNullOrEmpty(Surname) ||
                String.IsNullOrEmpty(Mail) ||
                String.IsNullOrEmpty(Password) || 
                String.IsNullOrEmpty(ProfilePhoto)) return false;
            else return true;
        }
        public static bool IsPasswordCorrect(String Password)
        {
            if(Password.Length < 8)
            {
                return false;
            }
            return true;
        }
        public void SignUp(String Name, String Surname, String Mail, DateTime BirthDate, String ProfilePhoto, CurrencyEnum DefaultCurrency, String StartCash, Gender Gender, String Password)
        {
            Boolean Convertable = true;
            UInt64 StartBalance;
            FileService fileService = new FileService();
            foreach(var elem in App.usersList)
            {
                if(Mail == elem.Mail)
                {
                    throw new Exception("This mail is already exist");
                }
            }
            if (SignUpService.AreParamsCorrect(Name, Surname, Mail, BirthDate, ProfilePhoto, DefaultCurrency, Gender, Password))
            { 
                Convertable = UInt64.TryParse(StartCash, out StartBalance);
                if (Convertable)
                {
                    UserModel userModel = new UserModel(Name, Surname, Mail, BirthDate, ProfilePhoto, DefaultCurrency, StartBalance, Gender, Password);
                    App.usersList.Add(userModel);
                    App.currentUser = userModel;
                    ++App.currentId;
                    fileService.Create(userModel);
                    fileService.SaveCurrentUser();
                    fileService.SaveId(App.currentId, @"C:\Money Manager\Common\Current Id.txt");
                    fileService.SaveUsersList();
                    App.history = new List<String>();
                    fileService.SaveHistory(@$"C:\Money Manager\Users\{App.currentUser.Id}\History.json");
                }
                else throw new WrongConvertationException("Cannot Convert");
            }
            else throw new NullSignUpException("Wrong parametres");
        }
        public void EditInfo(String Name, String Surname, String Mail, DateTime BirthDate, String ProfilePhoto, CurrencyEnum DefaultCurrency, Gender Gender, String Password)
        {
        //    Boolean Convertable = true;
        //    UInt64 StartBalance;
            FileService fileService = new FileService();
            foreach (var elem in App.usersList)
            {
                if (Mail == elem.Mail) throw new Exception("This mail is already exist");  
            }
            if (SignUpService.AreParamsCorrect(Name, Surname, Mail, BirthDate, ProfilePhoto, DefaultCurrency, Gender, Password))
            {
                UserModel userModel = new UserModel(Name, Surname, Mail, BirthDate, ProfilePhoto, DefaultCurrency, App.currentUser.Cash.Balance, Gender, Password); 
                App.currentUser = userModel;
                App.usersList[Int32.Parse((App.currentUser.Id - 1).ToString())] = userModel;
                fileService.SaveCurrentUser();
                fileService.SaveUsersList();
                fileService.SaveCurrentUserById();
            }
            else throw new Exception("Wrong parametres");
        } 
    }
}
