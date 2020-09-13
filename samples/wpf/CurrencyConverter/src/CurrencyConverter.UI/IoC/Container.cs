using CurrencyConverter.Model;
using CurrencyConverter.Services.IoC;
using CurrencyConverter.UI.DataProvider;
using CurrencyConverter.UI.DataProvider.Lookups;
using CurrencyConverter.UI.Navigation;
using CurrencyConverter.UI.ViewModels;
using CurrencyConverter.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using Prism.Events;

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
            services.AddScoped<SimpleNavigationService>();
            services.AddSingleton<IEventAggregator,EventAggregator>();

            services.AddTransient<History>();
            services.AddSingleton<HistoryViewModel>();

            services.AddTransient<TestView>();
            services.AddTransient<ITestViewModel,TestViewModel>();

            services.AddScoped<MainWindow>();
            services.AddScoped<MainViewModel>();
            services.AddTransient<SettingsWindow>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<ILookupProvider<Rate>, CurrencyTypeLookupProvider>();
            services.AddTransient<IExchangeRateDataProvider, ExchangeRateDataProvider>();







        }
    }
}