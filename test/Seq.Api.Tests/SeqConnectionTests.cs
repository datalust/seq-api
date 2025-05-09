using System.Net.Http;
using Xunit;

namespace Seq.Api.Tests
{
    public class SeqConnectionTests
    {
        [Fact]
        public void WhenConstructedTheHandlerConfigurationCallbackIsCalled()
        {
            var callCount = 0;

            using var _ = new SeqConnection(
                "https://test.example.com",
                apiKey: null,
                createHttpMessageHandler: cookies =>
                { 
                    ++callCount;
                    return new HttpClientHandler { CookieContainer = cookies };
                });

            Assert.Equal(1, callCount);
        }
    }
}
