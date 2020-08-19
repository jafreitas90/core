using CurrencyConverter.Model;
using CurrencyConverter.Services.IoC;
using CurrencyConverter.UI.DataProvider;
using CurrencyConverter.UI.DataProvider.Lookups;
using CurrencyConverter.UI.ViewModels;
using CurrencyConverter.UI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConverter.UI.IoC
{
    public static class Container
    {
        /// <summary>
        /// Register application services
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors</param>
        public static void ConfigureContainer(this IServiceCollection services)
        {
            services.ConfigureServiceContainer();
            services.AddScoped<MainWindow>();
            services.AddScoped<MainViewModel>();
            services.AddTransient<ILookupProvider<Rate>, CurrencyTypeLookupProvider>();
            services.AddTransient<IExchangeRateDataProvider, ExchangeRateDataProvider>();
        }
    }
}