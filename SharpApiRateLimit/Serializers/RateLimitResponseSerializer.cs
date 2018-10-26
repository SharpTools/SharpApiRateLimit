namespace SharpApiRateLimit {
    public class RateLimitResponseSerializer : IRateLimitResponseSerializer {
        public string ToJson(RateLimitResult result) {
            return $@"{{
    ""message"": ""API rate limit reached!"",
    ""period"": ""{result.RequestPeriod}"",
    ""limit"": ""{result.RequestLimit}"",
    ""retry-after"": ""{result.GetRateLimitResetAsString()}""
}}";
        }
    }
}