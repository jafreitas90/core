using System;
using CurrencyConverter.Model;

namespace CurrencyConverter.UI.Wrapper
{
    public class CurrencyWrapper : ModelWrapper<Rate>
    {
        public CurrencyWrapper(Rate rate) : base(rate)
        {
        }

        public string CurrencyType
        {
            get { return GetValue<string>(); }
            set
            {
                if (value != null && value != CurrencyType)
                {
                    SetValue(value);
                    CurrencyChangedEvent?.Invoke();
                }
            }
        }

        public decimal Value
        {
            get { return GetValue<decimal>(); }
            set
            {
                if(value != Value)
                {
                    SetValue(value);
                    CurrencyChangedEvent?.Invoke();
                }
            }
        }

        public event Action CurrencyChangedEvent;
    }
}