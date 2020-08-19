using CurrencyConverter.Infrastructure.Repositories;
using CurrencyConverter.Model;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConverter.Infrastructure.IoC
{
    public static class Container
    {
        /// <summary>
        /// Register application services
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors</param>
        public static void ConfigureInfrastructureContainer(this IServiceCollection services)
        {
            services.AddTransient<IExchangeRatesRepository, ExchangeRatesRepository>();
        }
    }
}