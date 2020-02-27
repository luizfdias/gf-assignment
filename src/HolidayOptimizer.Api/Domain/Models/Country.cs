namespace HolidayOptimizer.Api.Domain.Models
{
    public class Country
    {
        public string CountryCode { get; set; }

        public TimezoneUtc TimezoneUtc { get; set; }
    }

    public class TimezoneUtc
    {
        public int Hours { get; set; }

        public int Minutes { get; set; }
    }
}
