using FluentAssertions;
using HolidayOptimizer.Api.Contracts;
using HolidayOptimizer.Api.Domain.Enums;
using HolidayOptimizer.Api.Tests.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace HolidayOptimizer.Api.Tests.IntegrationTests
{
    public class HolidaysControllerTests
    {
        private readonly HttpClient _client;

        public HolidaysControllerTests()
        {
            _client = ClientFactory.Create();
        }

        [Fact]
        public async void GetHolidaysPerYearAndCountry_GivenTheCurrentYear_ShouldReturnHolidaysFromCache()
        {
            var response = await _client.GetAsync($"api/v1/holidays/{DateTime.UtcNow.Year}/NL");

            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            var jsonContent = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<HolidayResponse>>>(jsonContent);

            apiResponse.Data.Should().HaveCount(3);
        }

        [Fact]
        public async void GetHolidaysPerYearAndCountry_GivenYearDifferentFromCurrent_ShouldReturnHolidaysFromClient()
        {
            var response = await _client.GetAsync($"api/v1/holidays/2019/NL");

            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            var jsonContent = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<HolidayResponse>>>(jsonContent);

            apiResponse.Data.Should().HaveCount(10);
        }

        [Fact]
        public async void CountryMostHolidays_ShouldReturnCountryWithMostHolidays()
        {
            var response = await _client.GetAsync("api/v1/holidays/countryMostHolidays");

            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            
            var jsonContent = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<CountryMostHolidaysResponse>>(jsonContent);

            apiResponse.Data.CountryCode.Should().Be("NL");
            apiResponse.Data.HolidaysCount.Should().Be(3);
        }

        [Fact]
        public async void MonthMostHolidays_ShouldReturnMonthWithMostHolidays()
        {
            var response = await _client.GetAsync("api/v1/holidays/monthMostHolidays");

            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            var jsonContent = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<MonthWithMostHolidaysResponse>>(jsonContent);

            apiResponse.Data.Month.Should().Be(MonthsOfTheYear.December);
            apiResponse.Data.HolidaysCount.Should().Be(3);
        }

        [Fact]
        public async void CountryMostUniqueHolidays_ShouldReturnCountryWithMostUniqueHolidays()
        {
            var response = await _client.GetAsync("api/v1/holidays/countryMostUniqueHolidays");

            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            var jsonContent = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<CountryWithMostUniqueHolidaysResponse>>(jsonContent);

            apiResponse.Data.CountryCode.Should().Be("NL");
            apiResponse.Data.HolidaysCount.Should().Be(2);
        }
    }
}
