using System.Collections.Generic;

namespace HolidayOptimizer.Api.Contracts
{
    public class BiggestHolidaysSequenceResponse
    {
        public IEnumerable<HolidayHistoryResponse> HolidayHistories { get; }

        public BiggestHolidaysSequenceResponse(IEnumerable<HolidayHistoryResponse> holidayHistories)
        {
            HolidayHistories = holidayHistories;
        }
    }
}
