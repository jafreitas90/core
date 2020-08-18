using System.Threading;
using System.Threading.Tasks;
using CurrencyConverter.Model;

namespace CurrencyConverter.UI.DataProvider
{
    public class ExchangeRateDataProvider : IExchangeRateDataProvider
    {
        private readonly IExchangeRatesRepository _dataServiceCreator;

        public ExchangeRateDataProvider(IExchangeRatesRepository dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }

        public async Task<ExchangeRates> GetAllDataAsync(CancellationToken ct = default)
        {
            return await _dataServiceCreator.GetDataAsync(ct);
        }
    }
}
