using System;
using DependencyInjection.Sample1.Service1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Sample1.Client1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Inject dependencies
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IService1, Service>();
            serviceCollection.AddLogging(x => x.AddConsole());

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var service1 = serviceProvider.GetService<IService1>();
            service1.DoSometing();
            Console.ReadKey();
        }
    }
}
