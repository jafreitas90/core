using System.Threading;
using System.Threading.Tasks;

namespace CurrencyConverter.Model
{
    public interface IExchangeRatesRepository
    {
        Task SaveAsync(ExchangeRates rates, CancellationToken ct = default);
        Task<ExchangeRates> GetDataAsync(CancellationToken ct = default);
    }
}