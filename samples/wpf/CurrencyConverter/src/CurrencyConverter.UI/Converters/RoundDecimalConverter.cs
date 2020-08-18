using System;
using System.Windows.Data;

namespace CurrencyConverter.UI.Converters
{
    public class RoundDecimalConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!int.TryParse((string)parameter, out int parameterResult))
            {
                throw new InvalidOperationException("Parameter must be a int");
            }

            if (!(value is decimal))
            {
                throw new InvalidOperationException("Value must be a decimal");
            }

            return Decimal.Parse(Math.Round((decimal)value, (int)parameterResult).ToString("0.##"));
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value;
        }

        #endregion
    }
}