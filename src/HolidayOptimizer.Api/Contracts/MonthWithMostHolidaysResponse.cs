using HolidayOptimizer.Api.Domain.Enums;

namespace HolidayOptimizer.Api.Contracts
{
    public class MonthWithMostHolidaysResponse
    {
        public MonthsOfTheYear Month { get; }

        public int HolidaysCount { get; }

        public MonthWithMostHolidaysResponse(MonthsOfTheYear month, int holidaysCount)
        {
            Month = month;
            HolidaysCount = holidaysCount;
        }
    }
}
