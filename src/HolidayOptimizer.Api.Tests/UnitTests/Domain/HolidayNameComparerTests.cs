using FluentAssertions;
using HolidayOptimizer.Api.Domain;
using HolidayOptimizer.Api.Tests.AutoData;
using Xunit;

namespace HolidayOptimizer.Api.Tests.UnitTests.Domain
{
    public class HolidayNameComparerTests
    {
        [Theory, AutoNSubstituteData]
        public void Equals_WhenHolidaysHaveTheSameName_ShouldReturnTrue(HolidayNameComparer sut, Holiday holiday1, Holiday holiday2)
        {
            holiday1.Name = "Christmas";
            holiday2.Name = "Christmas";

            sut.Equals(holiday1, holiday2).Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]
        public void Equals_WhenHolidaysHaveADifferentName_ShouldReturnFalse(HolidayNameComparer sut, Holiday holiday1, Holiday holiday2)
        {
            holiday1.Name = "Christmas";
            holiday2.Name = "Easter's";

            sut.Equals(holiday1, holiday2).Should().BeFalse();
        }

        [Theory, AutoNSubstituteData]
        public void Equals_WhenFirstHolidayIsNull_ShouldReturnFalse(HolidayNameComparer sut, Holiday holiday)
        {
            sut.Equals(null, holiday).Should().BeFalse();
        }

        [Theory, AutoNSubstituteData]
        public void Equals_WhenSecondHolidayIsNull_ShouldReturnFalse(HolidayNameComparer sut, Holiday holiday)
        {
            sut.Equals(holiday, null).Should().BeFalse();
        }
    }
}
