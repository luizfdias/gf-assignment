using HolidayOptimizer.Api.Infrastructure.Interfaces;
using HolidayOptimizer.Api.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HolidayOptimizer.Api.Infrastructure.Clients
{
    public class HttpClientWrapper : IHttpClientWrapper
    {        
        private readonly HttpClient _httpClient;
        private readonly ISerialization _serializer;

        public HttpClientWrapper(            
            HttpClient httpClient,
            ISerialization serializer)
        {            
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, url);

            var result = await _httpClient.SendAsync(httpMessage);

            if (!result.IsSuccessStatusCode)
            {
                //Todo: handle this situation
                return default(TResponse);
            }

            var jsonContent = await result.Content.ReadAsStringAsync();
            
            return _serializer.Deserialize<TResponse>(jsonContent);
        }
    }
}
