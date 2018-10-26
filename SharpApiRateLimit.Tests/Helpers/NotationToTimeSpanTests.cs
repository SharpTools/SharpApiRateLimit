using System;
using Xunit;

namespace SharpApiRateLimit.Tests.Helpers {
    public class NotationToTimeSpanTests {

        [Theory]
        [InlineData("1s", 1)]
        [InlineData("10s", 10)]
        public void Should_parse_seconds(string notation, int value) {
            Assert.Equal(TimeSpan.FromSeconds(value), NotationToTimeSpan.Parse(notation));
        }

        [Theory]
        [InlineData("1m", 1)]
        [InlineData("10m", 10)]
        public void Should_parse_minutes(string notation, int value) {
            Assert.Equal(TimeSpan.FromMinutes(value), NotationToTimeSpan.Parse(notation));
        }

        [Theory]
        [InlineData("1h", 1)]
        [InlineData("10h", 10)]
        public void Should_parse_hours(string notation, int value) {
            Assert.Equal(TimeSpan.FromHours(value), NotationToTimeSpan.Parse(notation));
        }

        [Theory]
        [InlineData("1d", 1)]
        [InlineData("10d", 10)]
        public void Should_parse_days(string notation, int value) {
            Assert.Equal(TimeSpan.FromDays(value), NotationToTimeSpan.Parse(notation));
        }

        [Theory]
        [InlineData("1dd")]
        [InlineData("ad")]
        [InlineData("a")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Should_throw_on_error(string notation) {
            Assert.Throws<FormatException>(() => NotationToTimeSpan.Parse(notation));
        }
    }
}
