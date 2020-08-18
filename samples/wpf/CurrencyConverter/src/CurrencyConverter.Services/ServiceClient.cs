using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CurrencyConverter.Services
{
    class ServiceClient : IServiceClient
    {
        private readonly HttpClient _httpClient;

        public ServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetDataAsync<T>(string inputUrl, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(inputUrl, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var message = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(message);
            }

            throw new Exception($"Error getting data from: {inputUrl}, Status code: {response.StatusCode}");
        }
    }
}