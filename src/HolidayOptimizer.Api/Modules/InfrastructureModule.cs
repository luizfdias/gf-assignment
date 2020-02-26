using HolidayOptimizer.Api.Infrastructure.Clients;
using HolidayOptimizer.Api.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace HolidayOptimizer.Api.Modules
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(
            this IServiceCollection services)
        {
            services.AddSingleton<ISerialization, JsonSerializerWrapper>();
            services.AddMemoryCache();
            services.AddSingleton<HttpClient>();

            return services;
        }
    }
}
