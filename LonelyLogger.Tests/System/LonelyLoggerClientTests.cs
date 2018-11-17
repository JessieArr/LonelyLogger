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
            _SUT = new LonelyLoggerClient(new Uri("http://localhost:51396"));
        }

        [Fact]
        public async Task BasicSystemTest()
        {
            var result = await _SUT.PostLogAsync(new TestPayload() { message = "This is an automated test." });
            Assert.True(result.Succeeded);
        }

        private class TestPayload
        {
            public string message { get; set; }
        }
    }
}
