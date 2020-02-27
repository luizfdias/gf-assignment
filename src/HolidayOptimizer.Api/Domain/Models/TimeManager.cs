using System;

namespace HolidayOptimizer.Api.Domain.Models
{
    public class TimeManager
    {
        private DateTime _utcTime;

        public TimeManager()
        {
            _utcTime = new DateTime(2020, 1, 1, 0, 0, 0);
        }

        public void SpendTime(double hours)
        {
            _utcTime = _utcTime.AddHours(hours);
        }

        public DateTime GetLocalTime(Country country)
        {
            return _utcTime.AddHours(country.TimezoneUtc.Hours).AddMinutes(country.TimezoneUtc.Minutes);
        }

        public DateTime ConvertLocalToUct(Country country, DateTime localTime)
        {
            return localTime.AddHours(country.TimezoneUtc.Hours * -1).AddMinutes(country.TimezoneUtc.Minutes * -1);
        }

        public DateTime GetUtcTime()
        {
            return _utcTime;
        }

        public void TimeSkip(Country country, DateTime moveToDateTime)
        {
            SpendTime(moveToDateTime.Subtract(GetLocalTime(country)).TotalHours);
        }
    }
}
