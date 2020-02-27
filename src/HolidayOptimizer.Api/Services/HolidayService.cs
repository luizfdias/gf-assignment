using HolidayOptimizer.Api.Contracts;
using HolidayOptimizer.Api.Domain.Interfaces;
using HolidayOptimizer.Api.Domain.Models;
using HolidayOptimizer.Api.Services.ExternalContracts;
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
        private readonly IHttpClientWrapper _httpClient;
        private readonly IHolidaysSequenceService _holidaysSequenceService;
        private readonly string _holidayApiBaseUrl;

        public HolidayService(
            ICacheService cache,
            IHttpClientWrapper httpClient,
            IHolidaysSequenceService holidaysSequenceService,
            string holidayApiBaseUrl)
        {
            if (string.IsNullOrWhiteSpace(holidayApiBaseUrl))
            {
                throw new ArgumentNullException(nameof(holidayApiBaseUrl));
            }

            _holidayApiBaseUrl = holidayApiBaseUrl;

            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _holidaysSequenceService = holidaysSequenceService ?? throw new ArgumentNullException(nameof(holidaysSequenceService));
        }

        public Task<BiggestHolidaysSequenceResponse> GetBiggestHolidaysSequenceThisYear()
        {
            var holidayHistories = GetResult((currentYear, holidays)
                => _holidaysSequenceService.GetHolidaysSequence(holidays));

            var response = new BiggestHolidaysSequenceResponse(
                holidayHistories.GetBiggestHolidaySequence().Select(x => new HolidayPlanResponse
                {
                    StartDateUtc = x.StartDateUtc,
                    EndDateUtc = x.EndDateUtc,
                    LocalTimeArrival = x.StartDate,
                    LocalTimeEnd = x.EndDate,
                    Holiday = new HolidayResponse(x.Holiday.Date, x.Holiday.Name, x.Holiday.Country.CountryCode)
                }));

            return Task.FromResult(response);
        }

        public async Task<HolidaysPerYearAndCountryResponse> GetHolidaysPerYearAndCountry(HolidaysPerYearAndCountryRequest request)
        {            
            var allHolidays = _cache.Get<Holidays>($"holidays_{request.Year}");            

            if (allHolidays != null && allHolidays.Any())
            {
                var holidays = allHolidays.GetHolidaysPerYearAndCountry(request.Year, request.Country);

                return new HolidaysPerYearAndCountryResponse(
                    holidays.Select(x => new HolidayResponse(x.Date, x.Name, x.Country.CountryCode)));
            }
            else
            {
                var requestUrl = _holidayApiBaseUrl + $"{request.Year}/{request.Country}";

                var holidaysInfo = await _httpClient.GetAsync<IEnumerable<HolidayInfo>>(requestUrl);

                return new HolidaysPerYearAndCountryResponse(
                    holidaysInfo.Select(x => new HolidayResponse(x.Date, x.Name, x.CountryCode)));
            }                        
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
