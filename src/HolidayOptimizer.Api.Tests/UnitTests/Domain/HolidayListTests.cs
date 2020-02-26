using FluentAssertions;
using HolidayOptimizer.Api.Domain;
using HolidayOptimizer.Api.Domain.Enums;
using System;
using Xunit;

namespace HolidayOptimizer.Api.Tests.UnitTests
{
    public class HolidayListTests
    {
        [Fact]
        public void GetCountryWithMostHolidays_WhenHolidaysFound_ShouldReturnCountryWithMostHolidays()
        {
            var holidays = FeedHolidayList();

            var country = holidays.GetCountryWithMostHolidays(2020);

            country.CountryCode.Should().Be("NL");
            country.HolidaysCount.Should().Be(3);
        }

        [Fact]
        public void GetCountryWithMostHolidays_WhenHolidaysNotFound_ShouldReturnNull()
        {
            var holidays = FeedHolidayList();

            holidays.GetCountryWithMostHolidays(2021).Should().BeNull();
        }

        [Fact]
        public void GetMonthsMostHolidays_WhenHolidaysFound_ShouldReturnMonthWithMostHolidays()
        {
            var holidays = FeedHolidayList();

            holidays.GetMonthsMostHolidays(2020).Should().Be(MonthsOfTheYear.December);
        }

        [Fact]
        public void GetMonthsMostHolidays_WhenHolidaysNotFound_ShouldReturnUndefined()
        {
            var holidays = FeedHolidayList();

            holidays.GetMonthsMostHolidays(2021).Should().Be(MonthsOfTheYear.Undefined);
        }

        [Fact]
        public void GetCountryMostUniqueHolidays_WhenHolidaysFound_ShouldReturnCountryWithMostUniqueHolidays()
        {
            var holidays = FeedHolidayList();

            holidays.GetCountryMostUniqueHolidays(2020).Should().Be("NL");
        }

        private Holidays FeedHolidayList()
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
