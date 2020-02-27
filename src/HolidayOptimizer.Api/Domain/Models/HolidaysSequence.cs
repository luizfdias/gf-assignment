using System.Collections.Generic;
using System.Linq;

namespace HolidayOptimizer.Api.Domain.Models
{
    public class HolidaysSequence : List<HolidaySequenceItem>
    {
        public HolidaysSequence GetBiggestHolidaySequence()
        {
            var allSequences = new List<HolidaysSequence>();
            var result = new HolidaysSequence();

            for (int i = 0; i < this.Count; i++)
            {
                var holidayHistory = this[i];

                if (!result.Any())
                {
                    result.Add(holidayHistory);
                }

                if (i + 1 == this.Count)
                {
                    allSequences.Add(result);
                    break;
                }

                if (holidayHistory.EndDateUtc == this[i + 1].StartDateUtc)
                {
                    result.Add(this[i + 1]);
                }
                else
                {
                    allSequences.Add(result);
                    result = new HolidaysSequence();
                }
            }

            return allSequences.OrderByDescending(x => x.Count).FirstOrDefault();
        }
    }
}
