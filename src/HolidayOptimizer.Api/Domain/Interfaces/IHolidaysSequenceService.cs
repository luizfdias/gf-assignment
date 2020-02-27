using HolidayOptimizer.Api.Domain.Models;

namespace HolidayOptimizer.Api.Domain.Interfaces
{
    public interface IHolidaysSequenceService
    {
        HolidaysSequence GetHolidaysSequence(Holidays holidays);
    }
}
