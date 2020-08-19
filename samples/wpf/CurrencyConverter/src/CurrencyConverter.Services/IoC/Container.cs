using CurrencyConverter.Infrastructure.IoC;
using CurrencyConverter.Services.Synchronizers;
using CurrencyConverter.Services.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConverter.Services.IoC
{
    public static class Container
    {
        /// <summary>
        /// Register application services
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors</param>
        public static void ConfigureServiceContainer(this IServiceCollection services)
        {
            services.ConfigureInfrastructureContainer();
            services.AddConfiguration();
            services.AddSingleton<ApplicationSettings>();
            services.AddTransient<ISynchronizer, ExchangeRatesSynchronizer>();
            services.AddHttpClient<IServiceClient, ServiceClient>();
        }
    }
}