using HolidayOptimizer.Api.Contracts;
using System.Threading.Tasks;

namespace HolidayOptimizer.Api.Services.Interfaces
{
    public interface IHolidayService
    {
        Task<HolidaysPerYearAndCountryResponse> GetHolidaysPerYearAndCountry(HolidaysPerYearAndCountryRequest request);

        Task<BiggestHolidaysSequenceResponse> GetBiggestHolidaysSequenceThisYear();

        Task<CountryMostHolidaysResponse> GetCountryWithMostHolidaysThisYear();

        Task<MonthWithMostHolidaysResponse> GetMonthWithMostHolidaysThisYear();

        Task<CountryWithMostUniqueHolidaysResponse> GetCountryWithMostUniqueHolidaysThisYear();        
    }
}
