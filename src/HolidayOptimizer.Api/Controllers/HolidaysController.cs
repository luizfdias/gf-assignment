using System;
using HolidayOptimizer.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HolidayOptimizer.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HolidaysController : ApiBaseController
    {       
        private readonly ILogger<HolidaysController> _logger;
        private readonly IHolidayService _holidayService;

        public HolidaysController(
            ILogger<HolidaysController> logger,
            IHolidayService holidayService)
        {
            _logger = logger;
            _holidayService = holidayService ?? throw new ArgumentNullException(nameof(holidayService));
        }

        [HttpGet("CountryMostHolidays")]
        public IActionResult GetCountryWithMostHolidaysThisYear()
        {
            return Ok(_holidayService.GetCountryWithMostHolidaysThisYear());
        }

        [HttpGet("MonthMostHolidays")]
        public IActionResult GetMonthMostHolidays()
        {
            return Ok(_holidayService.GetMonthMostHolidaysThisYear());
        }

        [HttpGet("CountryMostUniqueHolidays")]
        public IActionResult GetCountryMostUniqueHolidays()
        {
            return Ok(_holidayService.GetCountryMostUniqueHolidaysThisYear());
        }
    }
}
