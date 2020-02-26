using FluentValidation;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace HolidayOptimizer.Api.Contracts
{
    public class HolidaysPerYearAndCountryRequest
    {
        public int Year { get; set; }

        public string Country { get; set; }
    }

    public class HolidaysPerYearAndCountryRequestValidation : AbstractValidator<HolidaysPerYearAndCountryRequest>
    {
        private readonly string[] _supportedCountryCodes;

        public HolidaysPerYearAndCountryRequestValidation(IConfiguration configuration)
        {
            _supportedCountryCodes = configuration.GetSection("SupportedCountryCodes").Get<string[]>();

            RuleFor(x => x.Country).Must(x => _supportedCountryCodes.Contains(x.ToUpper()))
                .WithErrorCode("900")
                .WithMessage("Country not supported.");
        }        
    }
}
