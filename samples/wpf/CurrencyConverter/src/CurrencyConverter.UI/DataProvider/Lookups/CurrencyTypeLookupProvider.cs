using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CurrencyConverter.Model;

namespace CurrencyConverter.UI.DataProvider.Lookups
{
    public class CurrencyTypeLookupProvider : ILookupProvider<Rate>
    {
        private readonly IExchangeRatesRepository _repository;

        public CurrencyTypeLookupProvider(IExchangeRatesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<string>> GetLookupAsync(CancellationToken ct = default)
        {
            var result = await _repository.GetDataAsync();
            return result?.Rates.Select(f => f.CurrencyType)
                    .OrderBy(l => l)
                    .ToList();
        }
    }
}