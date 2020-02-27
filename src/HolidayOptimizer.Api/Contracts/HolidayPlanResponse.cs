using System;

namespace HolidayOptimizer.Api.Contracts
{
    public class HolidayPlanResponse
    {
        public DateTime StartDateUtc { get; set; }

        public DateTime EndDateUtc { get; set; }

        public DateTime LocalTimeArrival { get; set; }

        public DateTime LocalTimeEnd { get; set; }

        public HolidayResponse Holiday { get; set; }
    }
}
