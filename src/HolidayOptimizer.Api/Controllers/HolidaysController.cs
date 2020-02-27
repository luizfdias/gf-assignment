using System;
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
            var result = await _holidayService.GetHolidaysPerYearAndCountry(request);

            return Ok(result.Holidays);
        }

        /// <summary>
        /// My assumption for this method was to find the biggest sequence of holidays in a year, considering that you are going
        /// to stay the max amount of time in each country you visit.
        /// The idea is avoid to spend too much of your super speed power, traveling less and enjoying your holidays more! =D
        /// </summary>
        /// <returns></returns>
        [HttpGet("BiggestHolidaysSequence")]
        public async Task<IActionResult> GetBiggestHolidaysSequence()
        {
            var result = await _holidayService.GetBiggestHolidaysSequenceThisYear();

            return Ok(result.HolidayHistories);
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
