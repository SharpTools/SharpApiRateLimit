using System;
using System.Linq;

namespace SharpApiRateLimit {
    public static class NotationToTimeSpan {
        public static TimeSpan Parse(string notation) {
            if(String.IsNullOrWhiteSpace(notation)) {
                throw GetException(notation);
            }
            var type = notation.Last();
            if (int.TryParse(notation.Substring(0, notation.Length - 1), out int number)) {
                switch (type) {
                    case 's': return TimeSpan.FromSeconds(number);
                    case 'm': return TimeSpan.FromMinutes(number);
                    case 'h': return TimeSpan.FromHours(number);
                    case 'd': return TimeSpan.FromDays(number);
                }
            }
            throw GetException(notation);
        }

        private static FormatException GetException(string notation) {
            return new FormatException("Could not parse notation " + notation);
        }
    }
}