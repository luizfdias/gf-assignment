namespace HolidayOptimizer.Api.Contracts
{
    public class CountryWithMostUniqueHolidaysResponse
    {
        public string CountryCode { get; }

        public int HolidaysCount { get; }

        public CountryWithMostUniqueHolidaysResponse(string countryCode, int holidaysCount)
        {
            CountryCode = countryCode;
            HolidaysCount = holidaysCount;
        }
    }
}
