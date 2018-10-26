using System;

namespace SharpApiRateLimit {
    public class RateLimitResultModel {
        public string RequestLimit { get; set; }
        public int RequestsRemaining { get; set; }
        public DateTime RateLimitReset { get; set; }
    }
}