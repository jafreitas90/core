using System.Threading;
using System.Threading.Tasks;

namespace CurrencyConverter.Model
{
    public interface IExchangeRatesRepository
    {
        /// <summary>
        /// Save ExchangeRates
        /// </summary>
        /// <param name="rates">ExchangeRates</param>
        /// <param name="ct">Propagates notification that operations should be canceled.</param>
        /// <returns>Task</returns>
        Task SaveAsync(ExchangeRates rates, CancellationToken ct = default);

        /// <summary>
        /// Get all data
        /// </summary>
        /// <param name="ct">Propagates notification that operations should be canceled.</param>
        /// <returns>ExchangeRates</returns>
        Task<ExchangeRates> GetDataAsync(CancellationToken ct = default);
    }
}