namespace HolidayOptimizer.Api.Infrastructure.Interfaces
{
    public interface ISerialization
    {
        string Serialize<TRequest>(TRequest content);

        TResponse Deserialize<TResponse>(string content);
    }
}
