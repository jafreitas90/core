using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyConverter.UI.DataProvider.Lookups
{
    public interface ILookupProvider<T>
    {
        Task<IEnumerable<string>> GetLookupAsync(CancellationToken ct = default);
    }
}
