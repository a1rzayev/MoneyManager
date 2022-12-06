using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MoneyManager.Classes;
using MoneyManager.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MoneyManager.Model
{
    [Serializable]
    public class UserModel
    { 
        public UInt64 Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Mail { get; set; }
        public String Password { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Card> PayWays { get; set; }
        public Dictionary<String, UInt64> SpendCategories { get; set; }
        public Dictionary<String, UInt64> IncomeCategories { get; set; }
        public Cash Cash { get; set; }
        public String ProfilePhoto { get; set; }
        public Gender Gender { get; set; }
        public CurrencyEnum DefaultCurrency { get; set; }
        public UserModel() { }
        public UserModel(String name, String surname, String mail, DateTime birthdate, String profilephoto, CurrencyEnum defaultcurrency, UInt64 startcash, Gender gender, String password)
        {
            Name = name;
            Surname = surname;
            Mail = mail;
            BirthDate = birthdate;
            ProfilePhoto = profilephoto;
            DefaultCurrency = defaultcurrency;
            Cash = new Cash(startcash, defaultcurrency);
            PayWays = new List<Card>();
            SpendCategories = new Dictionary<String, UInt64>() { { "Shopping", 0 }, { "Vacation", 0 }, { "Transport", 0 } };
            IncomeCategories = new Dictionary<String, UInt64>() {  { "Passive income", 0 }, { "Salary", 0 } };
            Gender = gender;
            Password = password;
            Id = App.currentId; 
        }
    }
}
