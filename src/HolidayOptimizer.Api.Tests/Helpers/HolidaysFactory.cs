using HolidayOptimizer.Api.Domain.Models;
using System;

namespace HolidayOptimizer.Api.Tests.Helpers
{
    public static class HolidaysFactory
    {
        public static Holidays Create()
        {
            var countryBR = new Country
            {
                CountryCode = "BR",
                TimezoneUtc = new TimezoneUtc { Hours = -2, Minutes = 0 }
            };

            var countryNL = new Country
            {
                CountryCode = "NL",
                TimezoneUtc = new TimezoneUtc { Hours = 1, Minutes = 0 }
            };

            var countryAT = new Country
            {
                CountryCode = "AT",
                TimezoneUtc = new TimezoneUtc { Hours = 1, Minutes = 0 }
            };

            var holidays = new Holidays();

            holidays.Add(new Holiday
            { Country = countryBR, Date = DateTime.Parse("2020-01-01"), Name = "New year" });
            holidays.Add(new Holiday
            { Country = countryNL, Date = DateTime.Parse("2020-01-01"), Name = "New year" });
            holidays.Add(new Holiday
            { Country = countryBR, Date = DateTime.Parse("2020-12-25"), Name = "Christimas" });
            holidays.Add(new Holiday
            { Country = countryBR, Date = DateTime.Parse("2020-07-09"), Name = "Independence day" });
            holidays.Add(new Holiday
            { Country = countryAT, Date = DateTime.Parse("2020-12-25"), Name = "Christimas" });
            holidays.Add(new Holiday
            { Country = countryNL, Date = DateTime.Parse("2020-04-27"), Name = "King's day" });
            holidays.Add(new Holiday
            { Country = countryNL, Date = DateTime.Parse("2020-05-05"), Name = "Freedom's day" });
            holidays.Add(new Holiday
            { Country = countryNL, Date = DateTime.Parse("2020-12-25"), Name = "Christimas" });

            return holidays;
        }
    }
}
