using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_CurrencyConverter.Model;

namespace Demo_CurrencyConverter.ViewModel
{
    public class MainPageViewModel
    {
        public CurrencyConverter currencyConverter { get; set; }
        private  List<string> _currencyNames { get; set; }
        public MainPageViewModel()
        {
            currencyConverter = new CurrencyConverter();
        }


        public List<string> CurrencyNames
        {
            get
            {
                return currencyConverter.Currencies.Select(c => c.Currency).ToList();
            }
        }

        

    }
}
