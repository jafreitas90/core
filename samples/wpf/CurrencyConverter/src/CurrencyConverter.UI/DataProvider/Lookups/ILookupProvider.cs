using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyConverter.UI.DataProvider.Lookups
{
    public interface ILookupProvider<T>
    {
        Task<IEnumerable<LookupItem>> GetLookupAsync(CancellationToken ct = default);
    }
}
