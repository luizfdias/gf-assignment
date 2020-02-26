using HolidayOptimizer.Api.Contracts;
using HolidayOptimizer.Api.Domain;
using HolidayOptimizer.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayOptimizer.Api.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly ICacheService _cache;
        private readonly IPublicHolidayClient _holidayClient;

        public HolidayService(
            ICacheService cache,
            IPublicHolidayClient holidayClient)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _holidayClient = holidayClient ?? throw new ArgumentNullException(nameof(holidayClient));
        }

        public async Task<HolidaysPerYearAndCountryResponse> GetHolidaysPerYearAndCountry(HolidaysPerYearAndCountryRequest request)
        {
            IEnumerable<Holiday> holidayResult;            

            var allHolidays = _cache.Get<Holidays>($"holidays_{request.Year}");

            if (allHolidays != null && allHolidays.Any())
            {
                holidayResult = allHolidays.GetHolidaysPerYearAndCountry(request.Year, request.Country);                
            }
            else
            {
                holidayResult = await _holidayClient.GetHolidays(request.Year, request.Country);
            }            

            return new HolidaysPerYearAndCountryResponse(
                    holidayResult.Select(x => new HolidayResponse(x.Date, x.Name, x.CountryCode)));
        }

        public Task<CountryMostHolidaysResponse> GetCountryWithMostHolidaysThisYear()
        {
            var country = GetResult((currentYear, holidays) 
                => holidays.GetCountryWithMostHolidays(currentYear));

            return Task.FromResult(new CountryMostHolidaysResponse(country.CountryCode, country.HolidaysCount));
        }

        public Task<MonthWithMostHolidaysResponse> GetMonthWithMostHolidaysThisYear()
        {
            var month = GetResult((currentYear, holidays) 
                => holidays.GetMonthsMostHolidays(currentYear));

            return Task.FromResult(new MonthWithMostHolidaysResponse(month.Month, month.HolidaysCount));
        }

        public Task<CountryWithMostUniqueHolidaysResponse> GetCountryWithMostUniqueHolidaysThisYear()
        {
            var country = GetResult((currentYear, holidays) 
                => holidays.GetCountryMostUniqueHolidays(currentYear));

            return Task.FromResult(new CountryWithMostUniqueHolidaysResponse(country.CountryCode, country.HolidaysCount));
        }

        private TResponse GetResult<TResponse>(Func<int, Holidays, TResponse> getDataFunc)
        {
            var currentYear = DateTime.UtcNow.Year;

            var holidays = _cache.Get<Holidays>($"holidays_{currentYear}");

            return getDataFunc(currentYear, holidays);
        }
    }
}
