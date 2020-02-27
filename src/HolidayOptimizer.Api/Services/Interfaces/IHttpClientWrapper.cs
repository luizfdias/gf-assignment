using System.Threading.Tasks;

namespace HolidayOptimizer.Api.Services.Interfaces
{
    public interface IHttpClientWrapper
    {
        Task<TResponse> GetAsync<TResponse>(string url);
    }
}
