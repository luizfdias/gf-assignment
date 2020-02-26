using HolidayOptimizer.Api.Domain;
using System;

namespace HolidayOptimizer.Api.Tests.Helpers
{
    public static class HolidaysFactory
    {
        public static Holidays Create()
        {
            var holidays = new Holidays();

            holidays.Add(new Holiday
            { CountryCode = "BR", Date = DateTime.Parse("2020-12-25"), Name = "Christimas" });
            holidays.Add(new Holiday
            { CountryCode = "BR", Date = DateTime.Parse("2020-07-09"), Name = "Independence day" });
            holidays.Add(new Holiday
            { CountryCode = "AT", Date = DateTime.Parse("2020-12-25"), Name = "Christimas" });
            holidays.Add(new Holiday
            { CountryCode = "NL", Date = DateTime.Parse("2020-04-27"), Name = "King's day" });
            holidays.Add(new Holiday
            { CountryCode = "NL", Date = DateTime.Parse("2020-05-05"), Name = "Freedom's day" });
            holidays.Add(new Holiday
            { CountryCode = "NL", Date = DateTime.Parse("2020-12-25"), Name = "Christimas" });

            return holidays;
        }
    }
}
