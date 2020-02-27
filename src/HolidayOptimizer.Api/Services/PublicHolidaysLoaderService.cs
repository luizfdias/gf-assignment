using HolidayOptimizer.Api.Domain.Models;
using HolidayOptimizer.Api.Services.ExternalContracts;
using HolidayOptimizer.Api.Services.Interfaces;
using HolidayOptimizer.Api.Services.Mappers;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HolidayOptimizer.Api.Services
{
    public class PublicHolidaysLoaderService : IHostedService
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly string[] _supportedCountryCodes;
        private readonly ICacheService _cache;
        private readonly string _holidayApiBaseUrl;
        private readonly string _countryInfoBaseUrl;

        public PublicHolidaysLoaderService(
            IHttpClientWrapper httpClient,
            string[] supportedCountryCodes,
            ICacheService cache,
            string holidayApiBaseUrl,
            string countryInfoBaseUrl)
        {
            if (string.IsNullOrWhiteSpace(holidayApiBaseUrl))
            {
                throw new ArgumentNullException(nameof(holidayApiBaseUrl));
            }

            _holidayApiBaseUrl = holidayApiBaseUrl;

            if (string.IsNullOrWhiteSpace(countryInfoBaseUrl))
            {
                throw new ArgumentNullException(nameof(countryInfoBaseUrl));
            }

            _countryInfoBaseUrl = countryInfoBaseUrl;

            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _supportedCountryCodes = supportedCountryCodes ?? throw new ArgumentNullException(nameof(supportedCountryCodes));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var currentYear = DateTime.UtcNow.Year;

            var holidaysList = new Holidays();

            foreach (var country in _supportedCountryCodes)
            {
                // During the startup of the application I am caching all the holidays from the current year. 
                // Unfortunatelly I didn't have time to manage some situations where we would need to update the cache
                // Like for example if the year changes, the cache wouldn't be updated, unless we restart the application

                var holidayApiRequestUrl = _holidayApiBaseUrl + $"{currentYear}/{country}";

                var holidaysInfo = await _httpClient.GetAsync<IEnumerable<HolidayInfo>>(holidayApiRequestUrl);

                var countryInfoApiRequestUrl = _countryInfoBaseUrl + country;

                /* I don't know why the API returns very different timezones for the same country, like the Netherlands, for example
                        ...
                        "timezones": [
                        "UTC-04:00",
                        "UTC+01:00" ]
                        ...
                    Since the last one is the one that makes more sense to me, I am always getting the last */
                var countryInfo = await _httpClient.GetAsync<CountryInfo>(countryInfoApiRequestUrl);

                holidaysList.AddRange(holidaysInfo.Select(x => new Holiday
                {
                    Date = x.Date,
                    Name = x.Name,
                    Country = CountryMapper.MapFromCountryInfo(countryInfo)
                }));
            }

            if (holidaysList.Any())
            {
                _cache.Set($"holidays_{currentYear}", holidaysList);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
