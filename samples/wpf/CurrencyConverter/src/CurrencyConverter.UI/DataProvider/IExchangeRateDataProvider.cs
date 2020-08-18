using System.Threading;
using System.Threading.Tasks;
using CurrencyConverter.Model;

namespace CurrencyConverter.UI.DataProvider
{
    public interface IExchangeRateDataProvider
    {
        Task<ExchangeRates> GetAllDataAsync(CancellationToken ct = default);
    }
}
