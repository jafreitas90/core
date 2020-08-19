using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConverter.Services.IoC
{
    public static class ConfigurationRegistry
    {
        /// <summary>
        /// Register application configuration
        /// </summary>
        /// <param name="services"> Specifies the contract for a collection of service descriptors</param>
        internal static void AddConfiguration(this IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory, "Configs"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var Configuration = builder.Build();
            services.AddSingleton<IConfiguration>(Configuration);
        }
    }
}