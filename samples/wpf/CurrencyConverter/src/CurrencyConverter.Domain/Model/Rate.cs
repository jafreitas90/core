using System;

namespace CurrencyConverter.Model
{
    public class Rate
    {
        public string CurrencyType { get; private set; }
        public decimal Value { get; private set; }

        public Rate(string currencyType, decimal value)
        {
            CurrencyType  = currencyType ?? throw new ArgumentNullException(nameof(currencyType));
            Value = (value < 0 ? throw new InvalidOperationException("Rate value cannot be lower than 0") : value);
        }
    }
}