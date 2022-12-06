using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Classes
{
    public class ExpirationDate
    {
        public Int32 Month { get; set; }
        public Int32 Year { get; set; }
        public ExpirationDate(Int32 _Month, Int32 _Year)
        {
            Month = _Month;
            Year = _Year;
        }
    }
}
