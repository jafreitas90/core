using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.UI.Navigation
{
    public interface IActivable
    {
        Task ActivateAsync(object parameter);
    }
}
