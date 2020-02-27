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
            if (request.Method == HttpMethod.Get && request.RequestUri.AbsoluteUri.Contains("https://date.nager.at/api/v2/publicholidays/"))
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("[{\"date\":\"2020-01-01\",\"localName\":\"Nieuwjaarsdag\",\"name\":\"New Year's Day\",\"countryCode\":\"NL\",\"fixed\":true,\"global\":true,\"counties\":null,\"launchYear\":1967,\"type\":\"Public\"},{\"date\":\"2020-04-10\",\"localName\":\"Goede Vrijdag\",\"name\":\"Good Friday\",\"countryCode\":\"NL\",\"fixed\":false,\"global\":true,\"counties\":null,\"launchYear\":null,\"type\":\"School, Authorities\"},{\"date\":\"2020-04-12\",\"localName\":\"Eerste Paasdag\",\"name\":\"Easter Sunday\",\"countryCode\":\"NL\",\"fixed\":false,\"global\":true,\"counties\":null,\"launchYear\":null,\"type\":\"Public\"},{\"date\":\"2020-04-13\",\"localName\":\"Tweede Paasdag\",\"name\":\"Easter Monday\",\"countryCode\":\"NL\",\"fixed\":false,\"global\":true,\"counties\":null,\"launchYear\":1642,\"type\":\"Public\"},{\"date\":\"2020-04-27\",\"localName\":\"Koningsdag\",\"name\":\"King's Day\",\"countryCode\":\"NL\",\"fixed\":true,\"global\":true,\"counties\":null,\"launchYear\":null,\"type\":\"Public\"},{\"date\":\"2020-05-05\",\"localName\":\"Bevrijdingsdag\",\"name\":\"Liberation Day\",\"countryCode\":\"NL\",\"fixed\":true,\"global\":true,\"counties\":null,\"launchYear\":1945,\"type\":\"School, Authorities\"},{\"date\":\"2020-05-21\",\"localName\":\"Hemelvaartsdag\",\"name\":\"Ascension Day\",\"countryCode\":\"NL\",\"fixed\":false,\"global\":true,\"counties\":null,\"launchYear\":null,\"type\":\"Public\"},{\"date\":\"2020-06-01\",\"localName\":\"Pinksteren\",\"name\":\"Whit Monday\",\"countryCode\":\"NL\",\"fixed\":false,\"global\":true,\"counties\":null,\"launchYear\":null,\"type\":\"Public\"},{\"date\":\"2020-12-25\",\"localName\":\"Eerste Kerstdag\",\"name\":\"Christmas Day\",\"countryCode\":\"NL\",\"fixed\":true,\"global\":true,\"counties\":null,\"launchYear\":null,\"type\":\"Public\"},{\"date\":\"2020-12-26\",\"localName\":\"Tweede Kerstdag\",\"name\":\"St. Stephen's Day\",\"countryCode\":\"NL\",\"fixed\":true,\"global\":true,\"counties\":null,\"launchYear\":null,\"type\":\"Public\"}]")
                };
            }

            if (request.Method == HttpMethod.Get && request.RequestUri.AbsoluteUri.Contains("https://restcountries.eu/rest/v2/alpha/"))
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"name\":\"Netherlands\",\"topLevelDomain\":[\".nl\"],\"alpha2Code\":\"NL\",\"alpha3Code\":\"NLD\",\"callingCodes\":[\"31\"],\"capital\":\"Amsterdam\",\"altSpellings\":[\"NL\",\"Holland\",\"Nederland\"],\"region\":\"Europe\",\"subregion\":\"Western Europe\",\"population\":17019800,\"latlng\":[52.5,5.75],\"demonym\":\"Dutch\",\"area\":41850.0,\"gini\":30.9,\"timezones\":[\"UTC-04:00\",\"UTC+01:00\"],\"borders\":[\"BEL\",\"DEU\"],\"nativeName\":\"Nederland\",\"numericCode\":\"528\",\"currencies\":[{\"code\":\"EUR\",\"name\":\"Euro\",\"symbol\":\"\u20AC\"}],\"languages\":[{\"iso639_1\":\"nl\",\"iso639_2\":\"nld\",\"name\":\"Dutch\",\"nativeName\":\"Nederlands\"}],\"translations\":{\"de\":\"Niederlande\",\"es\":\"Pa\u00EDses Bajos\",\"fr\":\"Pays-Bas\",\"ja\":\"\u30AA\u30E9\u30F3\u30C0\",\"it\":\"Paesi Bassi\",\"br\":\"Holanda\",\"pt\":\"Pa\u00EDses Baixos\",\"nl\":\"Nederland\",\"hr\":\"Nizozemska\",\"fa\":\"\u067E\u0627\u062F\u0634\u0627\u0647\u06CC \u0647\u0644\u0646\u062F\"},\"flag\":\"https:\\/\\/restcountries.eu\\/data\\/nld.svg\",\"regionalBlocs\":[{\"acronym\":\"EU\",\"name\":\"European Union\",\"otherAcronyms\":[],\"otherNames\":[]}],\"cioc\":\"NED\"}")
                };
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}
