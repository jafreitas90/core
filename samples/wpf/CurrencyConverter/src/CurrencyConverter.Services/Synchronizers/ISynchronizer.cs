using System.Threading;
using System.Threading.Tasks;

namespace CurrencyConverter.Services.Synchronizers
{
    /// <summary>
    /// Responsible for sync data with API
    /// </summary>
    public interface ISynchronizer
    {
        /// <summary>
        /// Sync data with provider
        /// </summary>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Task</returns>
        Task RetrieveAsync(CancellationToken cancellationToken = default);
    }
}