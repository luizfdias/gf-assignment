using FluentAssertions;
using HolidayOptimizer.Api.Domain.Models;
using System;
using System.Linq;
using Xunit;

namespace HolidayOptimizer.Api.Tests.UnitTests.Domain
{
    public class HolidaysSequenceTests
    {
        [Fact]
        public void GetBiggestHolidaySequence_ShouldFindTheBiggestSequenceAsExpected()
        {
            var sut = new HolidaysSequence();

            sut.Add(new HolidaySequenceItem { StartDateUtc = new DateTime(2020, 1, 1, 0, 0, 0), EndDateUtc = new DateTime(2020, 1, 2, 0, 0, 0) });
            sut.Add(new HolidaySequenceItem { StartDateUtc = new DateTime(2020, 1, 2, 0, 0, 0), EndDateUtc = new DateTime(2020, 1, 3, 5, 0, 0) });
            sut.Add(new HolidaySequenceItem { StartDateUtc = new DateTime(2020, 1, 3, 5, 0, 0), EndDateUtc = new DateTime(2020, 1, 4, 2, 0, 0) });
            sut.Add(new HolidaySequenceItem { StartDateUtc = new DateTime(2020, 1, 6, 12, 0, 0), EndDateUtc = new DateTime(2020, 1, 7, 0, 0, 0) });
            sut.Add(new HolidaySequenceItem { StartDateUtc = new DateTime(2020, 1, 7, 0, 0, 0), EndDateUtc = new DateTime(2020, 1, 8, 0, 0, 0) });

            var result = sut.GetBiggestHolidaySequence();

            result.Should().HaveCount(3);
        }

        [Fact]
        public void GetBiggestHolidaySequence_IfTheBiggestSequenceIsOne_ShouldReturnTheFirstFound()
        {
            var sut = new HolidaysSequence();

            sut.Add(new HolidaySequenceItem { StartDateUtc = new DateTime(2020, 1, 1, 0, 0, 0), EndDateUtc = new DateTime(2020, 1, 2, 0, 0, 0) });            
            sut.Add(new HolidaySequenceItem { StartDateUtc = new DateTime(2020, 1, 3, 5, 0, 0), EndDateUtc = new DateTime(2020, 1, 4, 2, 0, 0) });           
            sut.Add(new HolidaySequenceItem { StartDateUtc = new DateTime(2020, 1, 7, 0, 0, 0), EndDateUtc = new DateTime(2020, 1, 8, 0, 0, 0) });

            var result = sut.GetBiggestHolidaySequence();

            result.Should().HaveCount(1);

            result.FirstOrDefault().StartDateUtc.Should().Be(new DateTime(2020, 1, 1, 0, 0, 0));
        }
    }
}
