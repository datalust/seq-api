using System;
using System.Collections.Generic;
using Seq.Api.Model;
using Xunit;

namespace Seq.Api.Tests
{
    public class LinkTests
    {
        [Fact]
        public void ALinkWithNoParametersIsLiteral()
        {
            const string uri = "https://example.com";
            var link = new Link(uri);
            var constructed = link.GetUri();
            Assert.Equal(uri, constructed);
        }

        [Fact]
        public void AParameterizedLinkCanBeConstructed()
        {
            const string template = "https://example.com/{name}";
            var link = new Link(template);
            var constructed = link.GetUri(new Dictionary<string, object> {["name"] = "test"});
            Assert.Equal("https://example.com/test", constructed);
        }

        [Fact]
        public void InvalidParametersAreDetected()
        {
            const string template = "https://example.com";
            var link = new Link(template);
            Assert.Throws<ArgumentException>(() => link.GetUri(new Dictionary<string, object> {["name"] = "test"}));
        }
    }
}
