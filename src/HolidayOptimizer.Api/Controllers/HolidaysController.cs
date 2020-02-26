﻿using System;
using System.Threading.Tasks;
using HolidayOptimizer.Api.Contracts;
using HolidayOptimizer.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HolidayOptimizer.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HolidaysController : ApiBaseController
    {               
        private readonly IHolidayService _holidayService;

        public HolidaysController(
            IHolidayService holidayService)
        {
            _holidayService = holidayService ?? throw new ArgumentNullException(nameof(holidayService));
        }

        [HttpGet("{Year}/{Country}")]
        public async Task<IActionResult> GetHolidaysPerYearAndCountry([FromRoute]HolidaysPerYearAndCountryRequest request)
        {
            var holidaysResult = await _holidayService.GetHolidaysPerYearAndCountry(request);

            return Ok(holidaysResult.Holidays);
        }

        [HttpGet("CountryMostHolidays")]        
        public async Task<IActionResult> GetCountryWithMostHolidaysThisYear()
        {
            return Ok(await _holidayService.GetCountryWithMostHolidaysThisYear());
        }

        [HttpGet("MonthMostHolidays")]
        public async Task<IActionResult> GetMonthMostHolidays()
        {
            return Ok(await _holidayService.GetMonthWithMostHolidaysThisYear());
        }

        [HttpGet("CountryMostUniqueHolidays")]
        public async Task<IActionResult> GetCountryMostUniqueHolidays()
        {
            return Ok(await _holidayService.GetCountryWithMostUniqueHolidaysThisYear());
        }
    }
}
