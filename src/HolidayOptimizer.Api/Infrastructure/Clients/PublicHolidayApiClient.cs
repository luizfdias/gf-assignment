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
                //Todo: Do something
            }

            var jsonContent = await result.Content.ReadAsStringAsync();

            return _serializer.Deserialize<IEnumerable<Holiday>>(jsonContent);
        }
    }
}
