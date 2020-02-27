using System;

namespace HolidayOptimizer.Api.Services.ExternalContracts
{
    public class HolidayInfo
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public string CountryCode { get; set; }
    }
}
