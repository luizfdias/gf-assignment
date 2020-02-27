using HolidayOptimizer.Api.Domain.Models;
using HolidayOptimizer.Api.Services.ExternalContracts;
using System.Linq;

namespace HolidayOptimizer.Api.Services.Mappers
{
    public static class CountryMapper
    {
        public static Country MapFromCountryInfo(CountryInfo countryInfo)
        {
            var timezone = countryInfo.Timezones.Last();            
            timezone = timezone.Replace("UTC", string.Empty);

            var timezoneUtc = new TimezoneUtc();            

            if (!string.IsNullOrEmpty(timezone))
            {
                var signal = char.Parse(timezone.Substring(0, 1));
                
                timezoneUtc.Hours = int.Parse(timezone.Substring(1, 2));
                timezoneUtc.Minutes = int.Parse(timezone.Substring(4, 2));

                if (signal == '-')
                {
                    timezoneUtc.Hours = timezoneUtc.Hours * -1;
                    timezoneUtc.Minutes = timezoneUtc.Minutes * -1;
                }
            }

            return new Country
            {
                CountryCode = countryInfo.Alpha2Code,
                TimezoneUtc = timezoneUtc
            };
        }
    }
}
