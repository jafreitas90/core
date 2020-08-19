using System.Threading;
using System.Threading.Tasks;

namespace CurrencyConverter.Services
{
    public interface IServiceClient
    {
        /// <summary>
        /// Generic Synchronizer
        /// </summary>
        /// <typeparam name="T">Generic entity</typeparam>
        /// <param name="inputUrl">url</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled</param>
        /// <returns>Generic entity</returns>
        Task<T> GetDataAsync<T>(string inputUrl, CancellationToken cancellationToken = default);
    }
}