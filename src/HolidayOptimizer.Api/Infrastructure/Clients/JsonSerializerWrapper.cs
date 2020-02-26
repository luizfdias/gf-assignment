using HolidayOptimizer.Api.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace HolidayOptimizer.Api.Infrastructure.Clients
{
    public class JsonSerializerWrapper : ISerialization
    {
        public TResponse Deserialize<TResponse>(string content)
        {
            return JsonConvert.DeserializeObject<TResponse>(content);
        }

        public string Serialize<TRequest>(TRequest content)
        {
            return JsonConvert.SerializeObject(content);
        }
    }
}
