using HolidayOptimizer.Api.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HolidayOptimizer.Api.Domain
{
    public class Holidays : List<Holiday>
    {
        public Holidays GetHolidaysPerYearAndCountry(int year, string countryCode)
        {
            var holidays = new Holidays();

            var holidayList = this.Where(x
                => x.Date.Year == year
                && x.CountryCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase));

            holidays.AddRange(holidayList);

            return holidays;
        }

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

        public (MonthsOfTheYear Month, int HolidaysCount) GetMonthsMostHolidays(int year)
        {
            // In the requirements for the assignment doesn't say if I should consider same holidays in different countries in the sum or not.
            // For this reason, I assumed I should consider them for the sum.
            var holidaysOfTheYear = this.Where(x => x.Date.Year == year);

            if (!holidaysOfTheYear.Any())
            {
                return (MonthsOfTheYear.Undefined, 0);
            }

            var monthsGroup = this.GroupBy(x => x.Date.Month);

            var month = monthsGroup.OrderByDescending(x => x.Count()).First();

            return ((MonthsOfTheYear)month.Key, month.Count());
        }

        public (string CountryCode, int HolidaysCount) GetCountryMostUniqueHolidays(int year)
        {
            // There is probably a better way to do this, but I chose to not invest the time making the algorithm better
            var holidaysOfTheYear = this.Where(x => x.Date.Year == year);

            if (!holidaysOfTheYear.Any())
            {
                return (string.Empty, 0);
            }

            var holidayUniquesGroup = this
                .GroupBy(x => x.Name)
                .Where(x => x.Count() == 1)
                .Select(x => new Holiday { Name = x.Key });
            
            var countriesWithUniqueHolidays = this.Intersect(holidayUniquesGroup, new HolidayNameComparer());

            var countriesGroup = countriesWithUniqueHolidays.GroupBy(x => x.CountryCode);

            var country = countriesGroup.OrderByDescending(x => x.Count()).First();

            return (country.Key, country.Count());
        }        
    }
}
