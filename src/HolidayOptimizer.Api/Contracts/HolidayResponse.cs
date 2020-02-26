using System;

namespace HolidayOptimizer.Api.Contracts
{
    public class HolidayResponse
    {
        public DateTime Date { get; }

        public string Name { get; }

        public string CountryCode { get; }

        public HolidayResponse(DateTime date, string name, string countryCode)
        {
            Date = date;
            Name = name;
            CountryCode = countryCode;
        }
    }
}
