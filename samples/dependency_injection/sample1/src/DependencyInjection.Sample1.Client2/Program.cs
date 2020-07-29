using System;
using DependencyInjection.Sample1.Service1;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DependencyInjection.Sample1.Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Inject dependencies
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IService1, Service>();
            serviceCollection.AddLogging(provider =>
            {
                provider.AddSerilog(dispose: true);
            });

            Log.Logger = new LoggerConfiguration()
             .Enrich.FromLogContext()
             .WriteTo.Console()
             .CreateLogger();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var service1 = serviceProvider.GetService<IService1>();
            service1.DoSometing();
            Console.ReadKey();
        }
    }
}
