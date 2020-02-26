namespace HolidayOptimizer.Api.Contracts
{
    public class CountryMostHolidaysResponse
    {
        public string CountryCode { get; }

        public int HolidaysCount { get; }

        public CountryMostHolidaysResponse(string countryCode, int holidaysCount)
        {
            CountryCode = countryCode;
            HolidaysCount = holidaysCount;
        }
    }
}
