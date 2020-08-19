using CurrencyConverter.Shared.Extensions;
using Microsoft.Extensions.Configuration;

namespace CurrencyConverter.Services.Utilities
{
    /// <summary>
    /// Read configuration from appsettings
    /// </summary>
    public class ApplicationSettings
    {
        private IConfiguration _configuration;

        public ApplicationSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ExchangeRatesBaseAddress => _configuration.GetKey($"{Constants.ExchangeRates}:{Constants.BaseAddress}");
        public int NumberOfRetries => _configuration.GetKeyAsInt($"{Constants.ExchangeRates}:{Constants.NumberOfRetries}");
    }
}
