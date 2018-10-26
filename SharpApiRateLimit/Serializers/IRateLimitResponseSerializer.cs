namespace SharpApiRateLimit {
    public interface IRateLimitResponseSerializer {
        string ToJson(RateLimitResult result);
    }
}