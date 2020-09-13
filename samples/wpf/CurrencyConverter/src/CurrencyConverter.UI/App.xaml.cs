using System.Windows;
using CurrencyConverter.Services.Synchronizers;
using CurrencyConverter.UI.IoC;
using CurrencyConverter.UI.Navigation;
using CurrencyConverter.UI.ViewModels;
using CurrencyConverter.UI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConverter.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Inject dependencies
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureContainer();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Run the client application
            //var mainViewModel = serviceProvider.GetService<MainViewModel>();
            //MainWindow = new MainWindow();
            //MainWindow.Show();
            //_mainViewModel.Load();

            var navigationService = serviceProvider.GetRequiredService<SimpleNavigationService>();
            _ = navigationService.ShowAsync<MainWindow>();

            //var sync = serviceProvider.GetService<ISynchronizer>();
            //sync.RetrieveAsync();

        }
    }
}
