namespace HolidayOptimizer.Api.Services.Interfaces
{
    public interface ICacheService
    {
        TValue Get<TValue>(string key);

        void Set<TValue>(string key, TValue value);
    }
}
