using CurrencyConverter.Model;

namespace CurrencyConverter.UI.Wrapper
{
    public class ExchangeRatesWrappper : ModelWrapper<ExchangeRates>
    {
        public ExchangeRatesWrappper(ExchangeRates model) : base (model)
        {
        }

        public string Date
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public Rate Base
        {
            get { return GetValue<Rate>(); }
            set { SetValue(value); }
        }
    }
}