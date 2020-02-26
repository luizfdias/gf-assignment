using HolidayOptimizer.Api.Contracts;
using HolidayOptimizer.Api.Domain;
using HolidayOptimizer.Api.Domain.Enums;
using HolidayOptimizer.Api.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace HolidayOptimizer.Api.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IMemoryCache _cache;

        public HolidayService(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public CountryMostHolidaysResponse GetCountryWithMostHolidaysThisYear()
        {
            var country = GetResult((currentYear, holidays) 
                => holidays.GetCountryWithMostHolidays(currentYear));

            return new CountryMostHolidaysResponse(country.CountryCode, country.HolidaysCount);
        }

        public MonthsOfTheYear GetMonthMostHolidaysThisYear()
        {
            return GetResult((currentYear, holidays) 
                => holidays.GetMonthsMostHolidays(currentYear));
        }

        public string GetCountryMostUniqueHolidaysThisYear()
        {
            return GetResult((currentYear, holidays) 
                => holidays.GetCountryMostUniqueHolidays(currentYear));
        }

        private TResponse GetResult<TResponse>(Func<int, Holidays, TResponse> getDataFunc)
        {
            var currentYear = DateTime.UtcNow.Year;

            var holidays = _cache.Get<Holidays>($"holidays_{currentYear}");

            return getDataFunc(currentYear, holidays);
        }
    }
}
