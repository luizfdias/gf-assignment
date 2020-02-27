using HolidayOptimizer.Api.Domain.Interfaces;
using HolidayOptimizer.Api.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HolidayOptimizer.Api.Modules
{
    public static class DomainModule
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddSingleton<IHolidaysSequenceService, HolidaysSequenceService>();

            return services;
        }
    }
}
