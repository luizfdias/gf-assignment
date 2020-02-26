using HolidayOptimizer.Api.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace HolidayOptimizer.Api.Infrastructure.Caching
{
    public class MemoryCacheWrapper : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheWrapper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public TValue Get<TValue>(string key)
        {
            return (TValue)_memoryCache.Get(key);
        }

        public void Set<TValue>(string key, TValue value)
        {
            _memoryCache.Set(key, value);
        }
    }
}
