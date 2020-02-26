using HolidayOptimizer.Api.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayOptimizer.Api.Services.Interfaces
{
    public interface IPublicHolidayClient
    {
        Task<IEnumerable<Holiday>> GetHolidays(int year, string country);
    }
}
