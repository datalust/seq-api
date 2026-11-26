using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.ResourceGroups;
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

        [Fact]
        public async Task AFailedResourceGroupLoadIsRetriedOnTheNextCall()
        {
            var handler = new FailFirstResourceGroupRequestHandler();
            using var connection = new SeqConnection("https://test.example.com", createHttpMessageHandler: _ => handler);
            ILoadResourceGroup loader = connection;

            await Assert.ThrowsAsync<HttpRequestException>(() => loader.LoadResourceGroupAsync("Apps", CancellationToken.None));

            var group = await loader.LoadResourceGroupAsync("Apps", CancellationToken.None);

            Assert.NotNull(group);
            Assert.Equal(2, handler.ResourceGroupRequestCount);
        }

        class FailFirstResourceGroupRequestHandler : HttpMessageHandler
        {
            public int ResourceGroupRequestCount { get; private set; }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var path = request.RequestUri!.AbsolutePath;

                if (path == "/api")
                    return Task.FromResult(Json("{\"Links\":{\"AppsResources\":\"api/apps/resources\"}}"));

                if (path == "/api/apps/resources")
                {
                    ResourceGroupRequestCount++;
                    if (ResourceGroupRequestCount == 1)
                        throw new HttpRequestException("Connection reset by peer");

                    return Task.FromResult(Json("{\"Links\":{}}"));
                }

                throw new InvalidOperationException($"Unexpected request to {request.RequestUri}");
            }

            static HttpResponseMessage Json(string content) => new(HttpStatusCode.OK)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
        }
    }
}
