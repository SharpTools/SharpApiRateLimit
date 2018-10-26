using System;

namespace SharpApiRateLimit.Storage {
    public class CacheEntry {
        public DateTime AbsoluteExpiration { get; set; }
        public int Count { get; set; }
        public CacheEntry(DateTime absoluteExpiration) {
            AbsoluteExpiration = absoluteExpiration;
        }
    }
}