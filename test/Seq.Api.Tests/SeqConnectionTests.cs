using Xunit;

namespace Seq.Api.Tests
{
    public class SeqConnectionTests
    {
        [Fact]
        public void WhenConstructedTheHandlerConfigurationCallbackIsCalled()
        {
            var callCount = 0;

#pragma warning disable CS0618 // Type or member is obsolete
            using var _ = new SeqConnection("https://test.example.com", null, handler => { 
                Assert.NotNull(handler);
                ++callCount;
            });
#pragma warning restore CS0618 // Type or member is obsolete

            Assert.Equal(1, callCount);
        }
    }
}
