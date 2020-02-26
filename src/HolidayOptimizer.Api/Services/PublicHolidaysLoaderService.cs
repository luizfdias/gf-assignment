using HolidayOptimizer.Api.Domain;
using HolidayOptimizer.Api.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
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
        private readonly IPublicHolidayClient _holidayClient;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public PublicHolidaysLoaderService(
            IPublicHolidayClient holidayClient,
            IConfiguration configuration,
            IMemoryCache cache)
        {
            _holidayClient = holidayClient ?? throw new ArgumentNullException(nameof(holidayClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var currentYear = DateTime.UtcNow.Year;

            var countries = _configuration.GetSection("SupportedCountryCodes").Get<string[]>();

            var holidaysList = new Holidays();

            foreach (var country in countries)
            {
                var holidays = await _holidayClient.GetHolidays(currentYear, country);

                holidaysList.AddRange(holidays);
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
