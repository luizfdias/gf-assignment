using HolidayOptimizer.Api.Contracts;
using HolidayOptimizer.Api.Domain.Enums;

namespace HolidayOptimizer.Api.Services.Interfaces
{
    public interface IHolidayService
    {
        CountryMostHolidaysResponse GetCountryWithMostHolidaysThisYear();

        MonthsOfTheYear GetMonthMostHolidaysThisYear();

        string GetCountryMostUniqueHolidaysThisYear();
    }
}
