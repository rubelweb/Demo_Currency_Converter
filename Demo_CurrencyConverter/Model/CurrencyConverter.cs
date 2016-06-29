using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using System.Net;

namespace Demo_CurrencyConverter.Model
{
    public class CurrencyConverter : PropertyChangedBase
    {
        private string _currencySourceName;
        private string _currencyTargetName;
        private string _currencySourceAmount;
        private string _currencyTargetAmount;        
        private JObject _rawData;
        public string _updatedDate;
        public List<CurrencyIndex> Currencies = new List<CurrencyIndex>();     
        public CurrencyConverter()
        {
            Init();
        }
        private void Init()
        {
            using (WebClient webClient = new WebClient())
            {
                var currency = webClient.DownloadString("http://api.fixer.io/latest?base=ZAR");
                _rawData = JObject.Parse(currency);
                var rates = (JObject)_rawData["rates"];
                _updatedDate = (string)_rawData["date"];

                foreach (var obj in rates)
                {
                    Currencies.Add(new CurrencyIndex() { Currency = obj.Key, Rate = Convert.ToDecimal(obj.Value) });
                }
            }
        }
        public string CurrencySourceName
        {
            get
            {
                return this._currencySourceName;
            }
            set
            {
                this._currencySourceName = value;
                ConvertyCurrency();
                OnPropertyChanged();
            }
        }

        public string CurrencyTargetName
        {
            get
            {
                return this._currencyTargetName;
            }
            set
            {
                this._currencyTargetName = value;
                ConvertyCurrency();
                OnPropertyChanged();
            }
        }

        public string CurrencySourceAmount
        {
            get
            {
                return this._currencySourceAmount;
            }
            set
            {               
                    this._currencySourceAmount = value;                   
                    ConvertyCurrency();
                    OnPropertyChanged();
            }
        }
        public string CurrencyTargetAmount
        {
            get
            {
                return this._currencyTargetAmount;
            }
            set
            {
                this._currencyTargetAmount = value;
                OnPropertyChanged();
            }
        }
        private void ConvertyCurrency()
        {

            if (this._currencySourceAmount == null || this._currencySourceAmount == "")
            {
                CurrencyTargetAmount = "";
            }
            else 
            {
                decimal sourceAmount = Currencies.Where(c => c.Currency == this._currencySourceName).Select(c => c.Rate).FirstOrDefault();
                decimal targetAmount = Currencies.Where(c => c.Currency == this._currencyTargetName).Select(c => c.Rate).FirstOrDefault();
                CurrencyTargetAmount = Math.Round((decimal.Parse(this._currencySourceAmount) * (targetAmount * (1 / sourceAmount))), 5).ToString();
            }
        }
    }
}
