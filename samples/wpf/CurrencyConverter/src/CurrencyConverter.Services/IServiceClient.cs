using System.Threading;
using System.Threading.Tasks;

namespace CurrencyConverter.Services
{
    public interface IServiceClient
    {
        Task<T> GetDataAsync<T>(string inputUrl, CancellationToken cancellationToken = default);
    }
}