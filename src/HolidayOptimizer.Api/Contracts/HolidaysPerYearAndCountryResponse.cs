using System.Collections.Generic;

namespace HolidayOptimizer.Api.Contracts
{
    public class HolidaysPerYearAndCountryResponse
    {
        public IEnumerable<HolidayResponse> Holidays { get; }

        public HolidaysPerYearAndCountryResponse(IEnumerable<HolidayResponse> holidays)
        {
            Holidays = holidays;
        }
    }
}
