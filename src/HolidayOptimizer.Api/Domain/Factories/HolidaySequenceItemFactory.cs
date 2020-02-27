using HolidayOptimizer.Api.Domain.Models;
using System;

namespace HolidayOptimizer.Api.Domain.Factories
{
    public static class HolidaySequenceItemFactory
    {
        public static HolidaySequenceItem Create(Holiday holiday, TimeManager timeManager)
        {
            var localTime = timeManager.GetLocalTime(holiday.Country);

            var holidaySequenceItem = new HolidaySequenceItem
            {
                Holiday = holiday,
                StartDate = localTime,
                EndDate = new DateTime(holiday.Date.Year, holiday.Date.Month, holiday.Date.Day).AddDays(1),
                StartDateUtc = timeManager.GetUtcTime(),
                EndDateUtc = timeManager.ConvertLocalToUct(holiday.Country, new DateTime(localTime.Year, localTime.Month, localTime.Day).AddDays(1))
            };

            timeManager.SpendTime(holidaySequenceItem.EndDate.Subtract(localTime).TotalHours);

            return holidaySequenceItem;
        }
    }
}
