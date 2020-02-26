using HolidayOptimizer.Api.Domain;
using HolidayOptimizer.Api.Infrastructure.Interfaces;
using HolidayOptimizer.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HolidayOptimizer.Api.Infrastructure.Clients
{
    public class PublicHolidayApiClient : IPublicHolidayClient
    {
        private readonly string _publicHolidayApiBaseUrl;
        private readonly HttpClient _httpClient;
        private readonly ISerialization _serializer;

        public PublicHolidayApiClient(
            string publicHolidayApiBaseUrl,
            HttpClient httpClient,
            ISerialization serializer)
        {
            if (string.IsNullOrWhiteSpace(publicHolidayApiBaseUrl))
            {
                throw new ArgumentNullException(nameof(publicHolidayApiBaseUrl));
            }

            _publicHolidayApiBaseUrl = publicHolidayApiBaseUrl;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task<IEnumerable<Holiday>> GetHolidays(int year, string country)
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(new Uri(_publicHolidayApiBaseUrl), $"{year}/{country}"));

            var result = await _httpClient.SendAsync(httpMessage);

            if (!result.IsSuccessStatusCode)
            {
                //Todo: handle this situation
                return new List<Holiday>();
            }

            var jsonContent = await result.Content.ReadAsStringAsync();

            // Usually I have a specific model for the contract of third party apis
            // but for the assignment I kept simple, since my model is similar to the third party api.
            return _serializer.Deserialize<IEnumerable<Holiday>>(jsonContent);
        }
    }
}
