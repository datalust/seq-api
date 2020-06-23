using Xunit;

namespace Seq.Api.Tests
{
    public class SeqConnectionTests
    {
        [Fact]
        public void WhenConstructedTheHandlerConfigurationCallbackIsCalled()
        {
            var callCount = 0;

            using var _ = new SeqConnection("https://test.example.com", null, handler => { 
                Assert.NotNull(handler);
                ++callCount;
            });

            Assert.Equal(1, callCount);
        }
    }
}
