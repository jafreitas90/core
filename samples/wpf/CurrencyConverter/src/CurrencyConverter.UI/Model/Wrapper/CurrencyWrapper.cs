using CurrencyConverter.Model;
using CurrencyConverter.UI.Model.Enums;
using static CurrencyConverter.UI.Model.Delegates;

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
                    CurrencyChangedEvent?.Invoke(CurrencyChangePropertyEnum.CurrencyType);
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
                    CurrencyChangedEvent?.Invoke(CurrencyChangePropertyEnum.Value);
                }
            }
        }

        public event CurrencyChangeDelegate CurrencyChangedEvent;
    }
}