using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CurrencyConverter.Infrastructure.Utilities;
using CurrencyConverter.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CurrencyConverter.Infrastructure.Repositories
{
    /// <inheritdoc />
    public class ExchangeRatesRepository : IExchangeRatesRepository
    {
        private readonly ILogger<ExchangeRatesRepository> _logger;
        public ExchangeRatesRepository(ILogger<ExchangeRatesRepository> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public Task<ExchangeRates> GetDataAsync(CancellationToken ct = default)
        {
            try
            {
                string path = Constants.SyncFilePathRates;
                if (!File.Exists(path))
                    return Task.FromResult(default(ExchangeRates));
                   // return Task.FromResult<ExchangeRates>(null);

                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    return Task.FromResult(JsonConvert.DeserializeObject<ExchangeRates>(json));
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// I'm not using the potential of task because this implementation is not async, but the repository is ready to to be used with some async
        /// </summary>
        /// <param name="rates"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task SaveAsync(ExchangeRates rates, CancellationToken ct = default)
        {
            try
            {
                string path = Constants.SyncFilePathRates;

                Directory.CreateDirectory(Path.GetDirectoryName(path));
                using (StreamWriter file = File.CreateText(path))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(file, rates);
                }
                return Task.FromResult(0);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}