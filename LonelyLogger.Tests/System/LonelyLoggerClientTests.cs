using LonelyLogger.Client;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LonelyLogger.Tests.System
{
    public class LonelyLoggerClientTests
    {
        private LonelyLoggerClient _SUT;
        public LonelyLoggerClientTests()
        {
            _SUT = new LonelyLoggerClient(new Uri("http://localhost:5050"));
        }

        [Fact]
        public async Task BasicSystemTest()
        {
            var result = await _SUT.PostLogAsync(new TestPayload() { message = "This is an automated test." });
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task QueueingSystemTest()
        {
            var firstResult = await _SUT.PostLogAsync(new TestPayload() { message = "This is an automated test." });
            var secondResult = await _SUT.PostLogAsync(new TestPayload() { message = "This is an automated test." });
            Assert.True(secondResult.Result == LogResultEnum.Queued);
        }

        private class TestPayload
        {
            public string message { get; set; }
        }
    }
}
