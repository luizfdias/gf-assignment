using HolidayOptimizer.Api.Domain;
using HolidayOptimizer.Api.Services;
using HolidayOptimizer.Api.Services.Interfaces;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HolidayOptimizer.Api.Tests.UnitTests.Services
{
    public class PublicHolidaysLoaderServiceTests
    {
        private readonly PublicHolidaysLoaderService _sut;
        private readonly IPublicHolidayClient _publicHolidayClient;
        private readonly string[] _supportedCountries;
        private readonly ICacheService _cacheService;

        public PublicHolidaysLoaderServiceTests()
        {
            _publicHolidayClient = Substitute.For<IPublicHolidayClient>();

            _publicHolidayClient.GetHolidays(DateTime.UtcNow.Year, "BR").Returns(new List<Holiday> { new Holiday { CountryCode = "BR", Date = DateTime.Parse("2020-12-25"), Name = "Christmas" } });
            _publicHolidayClient.GetHolidays(DateTime.UtcNow.Year, "NL").Returns(new List<Holiday> { new Holiday { CountryCode = "NL", Date = DateTime.Parse("2020-12-25"), Name = "Christmas" } });
            _publicHolidayClient.GetHolidays(DateTime.UtcNow.Year, "AT").Returns(new List<Holiday> { new Holiday { CountryCode = "AT", Date = DateTime.Parse("2020-12-25"), Name = "Christmas" } });

            _cacheService = Substitute.For<ICacheService>();
            _supportedCountries = new string[] { "BR", "NL", "AT", "AB" };

            _sut = new PublicHolidaysLoaderService(
                _publicHolidayClient,
                _supportedCountries,
                _cacheService);
        }

        [Fact]
        public async Task StartAsync_ShouldLoadHolidaysDataAndSetInCache()
        {            
            await _sut.StartAsync(new CancellationToken());

            _cacheService.Received().Set($"holidays_{DateTime.UtcNow.Year}", Arg.Is<Holidays>(x => x.Count == 3));
        }
    }
}
