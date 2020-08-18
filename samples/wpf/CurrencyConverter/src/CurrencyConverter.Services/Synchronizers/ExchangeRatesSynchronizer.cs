using System;
using System.Threading;
using System.Threading.Tasks;
using CurrencyConverter.Model;
using CurrencyConverter.Services.Mappings;
using CurrencyConverter.Services.Models.Results;
using CurrencyConverter.Services.Utilities;
using Microsoft.Extensions.Logging;

namespace CurrencyConverter.Services.Synchronizers
{
    public class ExchangeRatesSynchronizer : ISynchronizer
    {
        private readonly IServiceClient _serviceClient;
        private readonly ILogger<ExchangeRatesSynchronizer> _logger;
        private readonly ApplicationSettings _applicationSettings;
        IExchangeRatesRepository _repository;

        public ExchangeRatesSynchronizer(IServiceClient serviceClient, ILogger<ExchangeRatesSynchronizer> logger, ApplicationSettings applicationSettings, IExchangeRatesRepository exchangeRatesRepository)
        {
            _serviceClient = serviceClient;
            _logger = logger;
            _applicationSettings = applicationSettings;
            _repository = exchangeRatesRepository;
        }

        /// <inheritdoc />
        public async Task RetrieveAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation($"Start retrieving exchange rates data.");

                var result = await _serviceClient.GetDataAsync<ExchangeRatesSynchronizerResult>(_applicationSettings.ExchangeRatesBaseAddress, cancellationToken);

                _logger.LogInformation($"Exchange rates data retrieved successfully.");

                await _repository.SaveAsync(result.ToExchangeRates(), cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving the data. Message: {ex.Message}");
                throw;
            }
        }
    }
}