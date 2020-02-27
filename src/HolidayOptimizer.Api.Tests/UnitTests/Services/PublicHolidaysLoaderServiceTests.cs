using HolidayOptimizer.Api.Domain;
using HolidayOptimizer.Api.Domain.Models;
using HolidayOptimizer.Api.Services;
using HolidayOptimizer.Api.Services.ExternalContracts;
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
        private readonly IHttpClientWrapper _httpClient;
        private readonly string[] _supportedCountries;
        private readonly ICacheService _cacheService;

        public PublicHolidaysLoaderServiceTests()
        {
            _httpClient = Substitute.For<IHttpClientWrapper>();

            _httpClient.GetAsync<IEnumerable<HolidayInfo>>($"https://date.nager.at/api/v2/PublicHolidays/{DateTime.UtcNow.Year}/BR")
                .Returns(new List<HolidayInfo> { new HolidayInfo { Date = DateTime.Parse("2020-12-25"), Name = "Christmas", CountryCode = "BR" } });

            _httpClient.GetAsync<IEnumerable<HolidayInfo>>($"https://date.nager.at/api/v2/PublicHolidays/{DateTime.UtcNow.Year}/NL")
                .Returns(new List<HolidayInfo> { new HolidayInfo { Date = DateTime.Parse("2020-12-25"), Name = "Christmas", CountryCode = "NL" } });

            _httpClient.GetAsync<IEnumerable<HolidayInfo>>($"https://date.nager.at/api/v2/PublicHolidays/{DateTime.UtcNow.Year}/AT")
                .Returns(new List<HolidayInfo> { new HolidayInfo { Date = DateTime.Parse("2020-12-25"), Name = "Christmas", CountryCode = "AT" } });

            var countryBR = new CountryInfo
            {                
                Timezones = new string[] { "UTC-02:00" }
            };

            var countryNL = new CountryInfo
            {               
                Timezones = new string[] { "UTC+01:00" }
            };

            var countryAT = new CountryInfo
            {                
                Timezones = new string[] { "UTC+01:00" }
            };

            _httpClient.GetAsync<CountryInfo>($"https://restcountries.eu/rest/v2/alpha/BR")
                .Returns(countryBR);

            _httpClient.GetAsync<CountryInfo>($"https://restcountries.eu/rest/v2/alpha/NL")
                .Returns(countryNL);

            _httpClient.GetAsync<CountryInfo>($"https://restcountries.eu/rest/v2/alpha/AT")
                .Returns(countryAT);

            _cacheService = Substitute.For<ICacheService>();
            _supportedCountries = new string[] { "BR", "NL", "AT", "AB" };

            _sut = new PublicHolidaysLoaderService(
                _httpClient,
                _supportedCountries,
                _cacheService,
                "https://date.nager.at/api/v2/PublicHolidays/",
                "https://restcountries.eu/rest/v2/alpha/");
        }

        [Fact]
        public async Task StartAsync_ShouldLoadHolidaysDataAndSetInCache()
        {            
            await _sut.StartAsync(new CancellationToken());

            _cacheService.Received().Set($"holidays_{DateTime.UtcNow.Year}", Arg.Is<Holidays>(x => x.Count == 3));
        }
    }
}
