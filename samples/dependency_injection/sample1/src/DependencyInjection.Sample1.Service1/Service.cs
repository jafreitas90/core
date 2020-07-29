using Microsoft.Extensions.Logging;

namespace DependencyInjection.Sample1.Service1
{
    public class Service : IService1
    {
        readonly ILogger _logger;

        public Service(ILogger<Service> logger)
        {
            _logger = logger;
        }

        public void DoSometing()
        {
            _logger.LogInformation("Logging!!!!");
        }
    }
}
