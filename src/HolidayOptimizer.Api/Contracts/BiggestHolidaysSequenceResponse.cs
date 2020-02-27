using System;
using System.Collections.Generic;
using System.Linq;

namespace HolidayOptimizer.Api.Contracts
{
    public class BiggestHolidaysSequenceResponse
    {
        public int Count { get { return HolidayPlan.Count(); } }

        public DateTime FirstHolidayDate { get { return HolidayPlan.Min(x => x.Holiday.Date); } }

        public DateTime LastHolidayDate { get { return HolidayPlan.Max(x => x.Holiday.Date); } }

        public IEnumerable<HolidayPlanResponse> HolidayPlan { get; }        

        public BiggestHolidaysSequenceResponse(IEnumerable<HolidayPlanResponse> holidayPlan)
        {
            HolidayPlan = holidayPlan;
        }
    }
}
