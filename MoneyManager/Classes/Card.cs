using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyManager.Helpers;

namespace MoneyManager.Classes
{
    public class Card : IPayType
    {
        public UInt64 Id { get; set; }
        public ExpirationDate ExpDate { get; set; }
        public UInt16 CVV { get; set; }
        public UInt64 Balance { get; set; }
        public CurrencyEnum Currency { get; set; }
        public CardCompanyEnum Company { get; set; }
        public Card(UInt64 id, ExpirationDate expdate, UInt16 cvv, UInt64 balance, CurrencyEnum currency, CardCompanyEnum company)
        {
            Id = id;
            ExpDate = expdate;
            CVV = cvv;
            Balance = balance;
            Currency = currency;
            Company = company;
        }
    }
}
