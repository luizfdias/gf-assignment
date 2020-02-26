using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HolidayOptimizer.Api.Tests.IntegrationTests.HttpMessageHandlers
{
    public class GetHolidaysHttpMessageHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Get)
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("[{\"date\":\"2020-01-01\",\"localName\":\"Nieuwjaarsdag\",\"name\":\"New Year's Day\",\"countryCode\":\"NL\",\"fixed\":true,\"global\":true,\"counties\":null,\"launchYear\":1967,\"type\":\"Public\"},{\"date\":\"2020-04-10\",\"localName\":\"Goede Vrijdag\",\"name\":\"Good Friday\",\"countryCode\":\"NL\",\"fixed\":false,\"global\":true,\"counties\":null,\"launchYear\":null,\"type\":\"School, Authorities\"},{\"date\":\"2020-04-12\",\"localName\":\"Eerste Paasdag\",\"name\":\"Easter Sunday\",\"countryCode\":\"NL\",\"fixed\":false,\"global\":true,\"counties\":null,\"launchYear\":null,\"type\":\"Public\"},{\"date\":\"2020-04-13\",\"localName\":\"Tweede Paasdag\",\"name\":\"Easter Monday\",\"countryCode\":\"NL\",\"fixed\":false,\"global\":true,\"counties\":null,\"launchYear\":1642,\"type\":\"Public\"},{\"date\":\"2020-04-27\",\"localName\":\"Koningsdag\",\"name\":\"King's Day\",\"countryCode\":\"NL\",\"fixed\":true,\"global\":true,\"counties\":null,\"launchYear\":null,\"type\":\"Public\"},{\"date\":\"2020-05-05\",\"localName\":\"Bevrijdingsdag\",\"name\":\"Liberation Day\",\"countryCode\":\"NL\",\"fixed\":true,\"global\":true,\"counties\":null,\"launchYear\":1945,\"type\":\"School, Authorities\"},{\"date\":\"2020-05-21\",\"localName\":\"Hemelvaartsdag\",\"name\":\"Ascension Day\",\"countryCode\":\"NL\",\"fixed\":false,\"global\":true,\"counties\":null,\"launchYear\":null,\"type\":\"Public\"},{\"date\":\"2020-06-01\",\"localName\":\"Pinksteren\",\"name\":\"Whit Monday\",\"countryCode\":\"NL\",\"fixed\":false,\"global\":true,\"counties\":null,\"launchYear\":null,\"type\":\"Public\"},{\"date\":\"2020-12-25\",\"localName\":\"Eerste Kerstdag\",\"name\":\"Christmas Day\",\"countryCode\":\"NL\",\"fixed\":true,\"global\":true,\"counties\":null,\"launchYear\":null,\"type\":\"Public\"},{\"date\":\"2020-12-26\",\"localName\":\"Tweede Kerstdag\",\"name\":\"St. Stephen's Day\",\"countryCode\":\"NL\",\"fixed\":true,\"global\":true,\"counties\":null,\"launchYear\":null,\"type\":\"Public\"}]")
                };
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}
