using FluentValidation;
using FluentValidation.AspNetCore;
using HolidayOptimizer.Api.Contracts;
using HolidayOptimizer.Api.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace HolidayOptimizer.Api.Modules
{
    public static class ApiModule
    {
        public static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(opt => opt.Filters.Add<ExceptionsFilter>())
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Holiday Optimizer API", Version = "v1" });
            });

            services.AddScoped<IValidator<HolidaysPerYearAndCountryRequest>, HolidaysPerYearAndCountryRequestValidation>();

            return services;
        }
    }
}
