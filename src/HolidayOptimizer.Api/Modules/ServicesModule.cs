using HolidayOptimizer.Api.Infrastructure.Clients;
using HolidayOptimizer.Api.Infrastructure.Interfaces;
using HolidayOptimizer.Api.Services;
using HolidayOptimizer.Api.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace HolidayOptimizer.Api.Modules
{
    public static class ServicesModule
    {
        public static IServiceCollection AddServicesModule(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHostedService<PublicHolidaysLoaderService>();
            services.AddSingleton<IHolidayService, HolidayService>();
            services.AddSingleton<IPublicHolidayClient>((ctx) 
                => new PublicHolidayApiClient(
                    configuration["PublicHolidayApiBaseUrl"],
                    ctx.GetService<HttpClient>(),
                    ctx.GetService<ISerialization>()));

            return services;
        }
    }
}
