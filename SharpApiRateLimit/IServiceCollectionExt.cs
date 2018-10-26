using Microsoft.Extensions.DependencyInjection;
using SharpApiRateLimit.Storage;

namespace SharpApiRateLimit {
    public static class IServiceCollectionExt {
        public static void AddRateLimit(this IServiceCollection services) {
            services.AddMemoryCache();
            services.AddSingleton<IRateLimitStorage, InMemoryRateLimitStorage>();
            services.AddSingleton<IRateLimitResponseSerializer, RateLimitResponseSerializer>();
        }
    }
}