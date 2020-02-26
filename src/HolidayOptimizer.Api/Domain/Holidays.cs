using HolidayOptimizer.Api.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace HolidayOptimizer.Api.Domain
{
    public class Holidays : List<Holiday>
    {
        public (string CountryCode, int HolidaysCount) GetCountryWithMostHolidays(int year)
        {
            var holidaysOfTheYear = this.Where(x => x.Date.Year == year);

            if (!holidaysOfTheYear.Any())
            {
                return (string.Empty, 0);
            }

            var countriesGroup = holidaysOfTheYear.GroupBy(x => x.CountryCode);

            var country = countriesGroup.OrderByDescending(x => x.Count()).First();

            return (country.Key, country.Count());
        }

        public MonthsOfTheYear GetMonthsMostHolidays(int year)
        {
            var holidaysOfTheYear = this.Where(x => x.Date.Year == year);

            if (!holidaysOfTheYear.Any())
            {
                return MonthsOfTheYear.Undefined;
            }

            var monthsGroup = this.GroupBy(x => x.Date.Month);

            return (MonthsOfTheYear)monthsGroup.OrderByDescending(x => x.Count()).First().Key;
        }

        public string GetCountryMostUniqueHolidays(int year)
        {
            var holidaysOfTheYear = this.Where(x => x.Date.Year == year);

            if (!holidaysOfTheYear.Any())
            {
                return null;
            }

            var holidayUniquesGroup = this
                .GroupBy(x => x.Name)
                .Where(x => x.Count() == 1)
                .Select(x => new Holiday { Name = x.Key });

            var countriesWithUniqueHolidays = this.Intersect(holidayUniquesGroup, new HolidayNameComparer());

            var countriesGroup = countriesWithUniqueHolidays.GroupBy(x => x.CountryCode);

            return countriesGroup.OrderByDescending(x => x.Count()).First().Key;
        }        
    }
}
