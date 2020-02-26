using HolidayOptimizer.Api.Domain;
using HolidayOptimizer.Api.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HolidayOptimizer.Api.Services
{
    public class PublicHolidaysLoaderService : IHostedService
    {
        private readonly IPublicHolidayClient _holidayClient;
        private readonly string[] _supportedCountryCodes;
        private readonly ICacheService _cache;

        public PublicHolidaysLoaderService(
            IPublicHolidayClient holidayClient,
            string[] supportedCountryCodes,
            ICacheService cache)
        {
            _holidayClient = holidayClient ?? throw new ArgumentNullException(nameof(holidayClient));
            _supportedCountryCodes = supportedCountryCodes ?? throw new ArgumentNullException(nameof(supportedCountryCodes));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var currentYear = DateTime.UtcNow.Year;            

            var holidaysList = new Holidays();

            foreach (var country in _supportedCountryCodes)
            {
                /// During the startup of the application I am caching all the holidays from the current year. 
                /// Unfortunatelly I didn't have time to manage some situations where we would need to update the cache
                /// Like for example if the year changes, the cache wouldn't be updated, unless we restart the application
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
