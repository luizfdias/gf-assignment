using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HolidayOptimizer.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HolidaysController : ControllerBase
    {       
        private readonly ILogger<HolidaysController> _logger;

        public HolidaysController(ILogger<HolidaysController> logger)
        {
            _logger = logger;
        }

        [HttpGet("CountryMostHolidays")]
        public IEnumerable<WeatherForecast> GetCountryWithMostHolidaysThisYear()
        {
            
        }

        [HttpGet("MonthMostHolidays")]
        public IEnumerable<WeatherForecast> GetCountryWithMostHolidaysThisYear()
        {

        }

        [HttpGet("CountryMostUniqueHolidays")]
        public IEnumerable<WeatherForecast> GetCountryWithMostHolidaysThisYear()
        {

        }
    }
}
