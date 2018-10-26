using Microsoft.Extensions.Caching.Memory;
using System;

namespace SharpApiRateLimit.Storage {
    public class InMemoryRateLimitStorage : IRateLimitStorage {
        private readonly IMemoryCache _memoryCache;

        public InMemoryRateLimitStorage(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        public RateLimitResult Process(string key, string period, int maxCalls) {
            var result = new RateLimitResult();

            var value = _memoryCache.GetOrCreate(key, c => {
                var expiration = DateTime.Now.Add(NotationToTimeSpan.Parse(period));
                var item = new CacheEntry(expiration);
                c.AbsoluteExpiration = expiration;
                c.Value = item;
                return item;
            });
            value.Count++;
            var remaining = maxCalls - value.Count;
            remaining = remaining > 0 ? remaining : 0;
            return new RateLimitResult {
                IsSuccess = value.Count <= maxCalls,
                RateLimitReset = value.AbsoluteExpiration,
                RequestLimit = maxCalls,
                RequestPeriod = period,
                RequestsRemaining = remaining 
            };
        }
    }
}
