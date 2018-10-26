namespace SharpApiRateLimit.Storage {
    public interface IRateLimitStorage {
        RateLimitResult Process(string key, string period, int calls);
    }
}