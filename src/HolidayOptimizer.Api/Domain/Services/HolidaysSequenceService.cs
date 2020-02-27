using HolidayOptimizer.Api.Domain.Factories;
using HolidayOptimizer.Api.Domain.Interfaces;
using HolidayOptimizer.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HolidayOptimizer.Api.Domain.Services
{
    public class HolidaysSequenceService : IHolidaysSequenceService
    {        
        public HolidaysSequence GetHolidaysSequence(Holidays holidays)
        {
            var timeManager = new TimeManager();

            var holidaySequences = new HolidaysSequence();

            var holidaysOrdered = holidays.OrderBy(x => x.Date)
                .ThenBy(x => x.Country.TimezoneUtc.Hours)
                .ThenBy(x => x.Country.TimezoneUtc.Minutes)
                .ToList();

            var currentHoliday = holidaysOrdered.LastOrDefault(x => x.Date == new DateTime(2020, 1, 1));

            while (currentHoliday != null)
            {
                currentHoliday = NextHoliday(currentHoliday, timeManager, holidaysOrdered, ref holidaySequences);
            }

            return holidaySequences;
        }

        private Holiday NextHoliday(
            Holiday currentHoliday, 
            TimeManager timeManager, 
            List<Holiday> holidays, 
            ref HolidaysSequence holidaySequences)
        {
            holidaySequences.Add(HolidaySequenceItemFactory.Create(currentHoliday, timeManager));

            var nextHoliday = GetNextHolidayInSequence(holidays, timeManager);

            if (nextHoliday == null)
            {
                nextHoliday = holidays.FirstOrDefault(x => x.Date.Date >= timeManager.GetLocalTime(x.Country).Date);

                if (nextHoliday == null)
                    return null;

                nextHoliday = holidays.LastOrDefault(x => x.Date.Date == nextHoliday.Date.Date);

                timeManager.TimeSkip(nextHoliday.Country, nextHoliday.Date);
            }

            return nextHoliday;
        }

        private Holiday GetNextHolidayInSequence(List<Holiday> holidays, TimeManager timeManager)
        {
            var nextHoliday = holidays.LastOrDefault(x =>
            {
                var localTime = timeManager.GetLocalTime(x.Country);

                return localTime.Date == x.Date;
            });

            return nextHoliday;
        }
    }
}
