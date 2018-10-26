using System;

namespace SharpApiRateLimit {
    public class RateLimitResult {
        public bool IsSuccess { get; set; }
        public string RequestPeriod { get; set; }
        public int RequestLimit { get; set; }
        public int RequestsRemaining { get; set; }
        public DateTime RateLimitReset { get; set; }
        public string GetRateLimitResetAsString() => RateLimitReset.ToUniversalTime().ToString("o");
        public long GetRateLimitResetAsUnixtime() => (long) RateLimitReset.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }
}