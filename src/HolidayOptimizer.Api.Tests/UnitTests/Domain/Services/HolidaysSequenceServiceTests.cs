using FluentAssertions;
using HolidayOptimizer.Api.Domain.Models;
using HolidayOptimizer.Api.Domain.Services;
using HolidayOptimizer.Api.Tests.AutoData;
using System;
using Xunit;

namespace HolidayOptimizer.Api.Tests.UnitTests.Domain.Services
{
    public class HolidaysSequenceServiceTests
    {
        [Theory, AutoNSubstituteData]
        public void GetHolidaysSequence_WhenTwoHolidaysAreFoundInSequence_ShouldReturnThem(HolidaysSequenceService sut)
        {
            var holidays = TwoHolidaysInARow();

            var result = sut.GetHolidaysSequence(holidays);

            result.Count.Should().Be(2);

            result[0].Holiday.Country.CountryCode.Should().Be("NZ");
            result[1].Holiday.Country.CountryCode.Should().Be("NZ");
        }

        [Theory, AutoNSubstituteData]
        public void GetHolidaysSequence_WhenThreeHolidaysAreFoundInSequence_ShouldReturnThem(HolidaysSequenceService sut)
        {
            var holidays = ThreeHolidaysInARow();

            var result = sut.GetHolidaysSequence(holidays);

            result.Count.Should().Be(3);

            result[0].Holiday.Country.CountryCode.Should().Be("NZ");
            result[1].Holiday.Country.CountryCode.Should().Be("NZ");
            result[2].Holiday.Country.CountryCode.Should().Be("BR");
        }

        [Theory, AutoNSubstituteData]
        public void GetHolidaysSequence_WhenEachSequenceHasJustOneHoliday_ShouldReturnThem(HolidaysSequenceService sut)
        {
            var holidays = NoHolidaysInSequence();

            var result = sut.GetHolidaysSequence(holidays);

            result.Count.Should().Be(3);
        }

        private static Holidays TwoHolidaysInARow()
        {
            var holidays = new Holidays();

            holidays.Add(new Holiday
            {
                Date = new DateTime(2020, 1, 1),
                Country = new Country
                {
                    CountryCode = "NZ",
                    TimezoneUtc = new TimezoneUtc
                    {
                        Hours = 13,
                        Minutes = 0
                    }
                }
            });

            holidays.Add(new Holiday
            {
                Date = new DateTime(2020, 1, 1),
                Country = new Country
                {
                    CountryCode = "BR",
                    TimezoneUtc = new TimezoneUtc
                    {
                        Hours = -2,
                        Minutes = 0
                    }
                }
            });

            holidays.Add(new Holiday
            {
                Date = new DateTime(2020, 1, 2),
                Country = new Country
                {
                    CountryCode = "NZ",
                    TimezoneUtc = new TimezoneUtc
                    {
                        Hours = 13,
                        Minutes = 0
                    }
                }
            });

            return holidays;
        }

        private static Holidays ThreeHolidaysInARow()
        {
            var holidays = new Holidays();

            holidays.Add(new Holiday
            {
                Date = new DateTime(2020, 1, 1),
                Country = new Country
                {
                    CountryCode = "NZ",
                    TimezoneUtc = new TimezoneUtc
                    {
                        Hours = 13,
                        Minutes = 0
                    }
                }
            });

            holidays.Add(new Holiday
            {
                Date = new DateTime(2020, 1, 2),
                Country = new Country
                {
                    CountryCode = "BR",
                    TimezoneUtc = new TimezoneUtc
                    {
                        Hours = -2,
                        Minutes = 0
                    }
                }
            });

            holidays.Add(new Holiday
            {
                Date = new DateTime(2020, 1, 2),
                Country = new Country
                {
                    CountryCode = "NZ",
                    TimezoneUtc = new TimezoneUtc
                    {
                        Hours = 13,
                        Minutes = 0
                    }
                }
            });

            return holidays;
        }

        private static Holidays NoHolidaysInSequence()
        {
            var holidays = new Holidays();

            holidays.Add(new Holiday
            {
                Date = new DateTime(2020, 1, 1),
                Country = new Country
                {
                    CountryCode = "NZ",
                    TimezoneUtc = new TimezoneUtc
                    {
                        Hours = 13,
                        Minutes = 0
                    }
                }
            });

            holidays.Add(new Holiday
            {
                Date = new DateTime(2020, 1, 5),
                Country = new Country
                {
                    CountryCode = "BR",
                    TimezoneUtc = new TimezoneUtc
                    {
                        Hours = -2,
                        Minutes = 0
                    }
                }
            });

            holidays.Add(new Holiday
            {
                Date = new DateTime(2020, 1, 10),
                Country = new Country
                {
                    CountryCode = "NZ",
                    TimezoneUtc = new TimezoneUtc
                    {
                        Hours = 13,
                        Minutes = 0
                    }
                }
            });

            return holidays;
        }
    }
}
