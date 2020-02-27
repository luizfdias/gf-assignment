using FluentAssertions;
using HolidayOptimizer.Api.Domain.Enums;
using HolidayOptimizer.Api.Tests.Helpers;
using Xunit;

namespace HolidayOptimizer.Api.Tests.UnitTests
{
    public class HolidaysTests
    {
        [Fact]
        public void GetHolidaysPerYearAndCountry_GivenAYearAndCountry_ShouldReturnExpectedHolidays()
        {
            var allHolidays = HolidaysFactory.Create();

            var holidaysResult = allHolidays.GetHolidaysPerYearAndCountry(2020, "NL");

            holidaysResult.Should().HaveCount(4);
        }

        [Fact]
        public void GetCountryWithMostHolidays_WhenHolidaysFound_ShouldReturnCountryWithMostHolidays()
        {
            var holidays = HolidaysFactory.Create();

            var country = holidays.GetCountryWithMostHolidays(2020);

            country.CountryCode.Should().Be("NL");
            country.HolidaysCount.Should().Be(4);
        }

        [Fact]
        public void GetCountryWithMostHolidays_WhenHolidaysNotFound_ShouldReturnNull()
        {
            var holidays = HolidaysFactory.Create();

            var country = holidays.GetCountryWithMostHolidays(2021);

            country.CountryCode.Should().BeEmpty();
            country.HolidaysCount.Should().Be(0);
        }

        [Fact]
        public void GetMonthsMostHolidays_WhenHolidaysFound_ShouldReturnMonthWithMostHolidays()
        {
            var holidays = HolidaysFactory.Create();

            var month = holidays.GetMonthsMostHolidays(2020);

            month.Month.Should().Be(MonthsOfTheYear.December);
            month.HolidaysCount.Should().Be(3);
        }

        [Fact]
        public void GetMonthsMostHolidays_WhenHolidaysNotFound_ShouldReturnUndefined()
        {
            var holidays = HolidaysFactory.Create();

            var month = holidays.GetMonthsMostHolidays(2021);

            month.Month.Should().Be(MonthsOfTheYear.Undefined);
            month.HolidaysCount.Should().Be(0);
        }

        [Fact]
        public void GetCountryMostUniqueHolidays_WhenHolidaysFound_ShouldReturnCountryWithMostUniqueHolidays()
        {
            var holidays = HolidaysFactory.Create();

            var country = holidays.GetCountryMostUniqueHolidays(2020);

            country.CountryCode.Should().Be("NL");
            country.HolidaysCount.Should().Be(2);
        }        
    }
}
