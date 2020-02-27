using System;

namespace HolidayOptimizer.Api.Domain.Models
{
    public class HolidaySequenceItem
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDateUtc { get; set; }

        public DateTime EndDateUtc { get; set; }

        public Holiday Holiday { get; set; }
    }
}
