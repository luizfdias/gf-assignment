using HolidayOptimizer.Api.Domain.Interfaces;
using HolidayOptimizer.Api.Infrastructure.Caching;
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
            services.AddHostedService((ctx) 
                => new PublicHolidaysLoaderService(
                    ctx.GetService<IHttpClientWrapper>(),
                    configuration.GetSection("SupportedCountryCodes").Get<string[]>(),
                    ctx.GetService<ICacheService>(),
                    configuration["PublicHolidayApiBaseUrl"],
                    configuration["CountryInfoApiBaseUrl"]));

            services.AddSingleton<ICacheService, MemoryCacheWrapper>();
            services.AddSingleton<IHolidayService>((ctx) 
                => new HolidayService(
                    ctx.GetService<ICacheService>(),
                    ctx.GetService<IHttpClientWrapper>(),
                    ctx.GetService<IHolidaysSequenceService>(),
                    configuration["PublicHolidayApiBaseUrl"]));

            services.AddSingleton<IHttpClientWrapper>((ctx) 
                => new HttpClientWrapper(                    
                    ctx.GetService<HttpClient>(),
                    ctx.GetService<ISerialization>()));

            return services;
        }
    }
}
