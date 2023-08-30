// Copyright © Datalust and contributors. 
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Seq.Api.Model;
using Seq.Api.Model.Root;
using Seq.Api.Serialization;
using System.Threading;
using Seq.Api.Streams;
using System.Net.WebSockets;

namespace Seq.Api.Client
{
    /// <summary>
    /// A low-level client that provides navigation over the linked resource structure of the Seq HTTP API.
    /// </summary>
    public sealed class SeqApiClient : IDisposable
    {
        readonly string _apiKey;

        // Future versions of Seq may not completely support vN-1 features, however
        // providing this as an Accept header will ensure what compatibility is available
        // can be utilized.
        const string SeqApiV9MediaType = "application/vnd.datalust.seq.v9+json";

        readonly CookieContainer _cookies = new();
        readonly JsonSerializer _serializer = JsonSerializer.Create(
            new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter(), new LinkCollectionConverter() },
                DateParseHandling = DateParseHandling.None,
                FloatParseHandling = FloatParseHandling.Decimal
            });

        /// <summary>
        /// Construct a <see cref="SeqApiClient"/>.
        /// </summary>
        /// <param name="serverUrl">The base URL of the Seq server.</param>
        /// <param name="apiKey">An API key to use when making requests to the server, if required.</param>
        /// <param name="useDefaultCredentials">Whether default credentials will be sent with HTTP requests; the default is <c>true</c>.</param>
        [Obsolete("Prefer `SeqApiClient(serverUrl, apiKey, createHttpMessageHandler)` instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public SeqApiClient(string serverUrl, string apiKey, bool useDefaultCredentials)
            : this(serverUrl, apiKey, handler => handler.UseDefaultCredentials = useDefaultCredentials)
        {
        }

        /// <summary>
        /// Construct a <see cref="SeqApiClient"/>.
        /// </summary>
        /// <param name="serverUrl">The base URL of the Seq server.</param>
        /// <param name="apiKey">An API key to use when making requests to the server, if required.</param>
        /// <param name="configureHttpClientHandler">An optional callback to configure the <see cref="HttpClientHandler"/> used when making HTTP requests
        /// to the Seq API.</param>
        [Obsolete("Prefer `SeqApiClient(serverUrl, apiKey, createHttpMessageHandler)` instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public SeqApiClient(string serverUrl, string apiKey, Action<HttpClientHandler> configureHttpClientHandler)
            : this(serverUrl, apiKey, cookies =>
            {
                var handler = new HttpClientHandler { CookieContainer = cookies };
                configureHttpClientHandler?.Invoke(handler);
                return handler;
            })
        {
        }

        /// <summary>
        /// Construct a <see cref="SeqApiClient"/>.
        /// </summary>
        /// <param name="serverUrl">The base URL of the Seq server.</param>
        /// <param name="apiKey">An API key to use when making requests to the server, if required.</param>
        /// <param name="createHttpMessageHandler">An optional callback to construct the HTTP message handler used when making requests
        /// to the Seq API. The callback receives a <see cref="CookieContainer"/> that is shared with WebSocket requests made by the client.</param>
        public SeqApiClient(string serverUrl, string apiKey = null, Func<CookieContainer, HttpMessageHandler> createHttpMessageHandler = null)
        {
            // This is required for compatibility with the obsolete constructor, which we can remove sometime in 2024.
            var httpMessageHandler = createHttpMessageHandler?.Invoke(_cookies) ??
#if SOCKETS_HTTP_HANDLER
                                     new SocketsHttpHandler { CookieContainer = _cookies };
#else
                                     new HttpClientHandler { CookieContainer = _cookies };
#endif
            
            ServerUrl = serverUrl ?? throw new ArgumentNullException(nameof(serverUrl));

            if (!string.IsNullOrEmpty(apiKey))
                _apiKey = apiKey;
            
            var baseAddress = serverUrl;
            if (!baseAddress.EndsWith("/"))
                baseAddress += "/";

            HttpClient = new HttpClient(httpMessageHandler);
            HttpClient.BaseAddress = new Uri(baseAddress);
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(SeqApiV9MediaType));

            if (_apiKey != null)
                HttpClient.DefaultRequestHeaders.Add("X-Seq-ApiKey", _apiKey);
        }

        /// <summary>
        /// The base URL of the Seq server.
        /// </summary>
        public string ServerUrl { get; }

        /// <summary>
        /// The HTTP client used when making requests to the Seq server. The HTTP client is configured with the server's base address, collects any cookies
        /// sent with responses from the API, and will send the appropriate <c>Accept</c> and <c>X-Seq-ApiKey</c> headers by default.
        /// </summary>
        public HttpClient HttpClient { get; }

        /// <summary>
        /// Get the root entity describing the server and providing links to other resources available from the API.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> supporting cancellation.</param>
        /// <returns>The root entity.</returns>
        public Task<RootEntity> GetRootAsync(CancellationToken cancellationToken = default)
        {
            return HttpGetAsync<RootEntity>("api", cancellationToken);
        }

        /// <summary>
        /// Issue a <c>GET</c> request returning a <typeparamref name="TEntity"/> by following the <paramref name="link"/> from <paramref name="entity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type into which the response should be deserialized.</typeparam>
        /// <param name="entity">An entity previously retrieved from the API.</param>
        /// <param name="link">The name of the outbound link template present in <paramref name="entity"/>'s <see cref="ILinked.Links"/> collection.</param>
        /// <param name="parameters">Named parameters to substitute into the link template, if required.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> supporting cancellation.</param>
        /// <returns>The response entity.</returns>
        public Task<TEntity> GetAsync<TEntity>(ILinked entity, string link, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            return HttpGetAsync<TEntity>(linkUri, cancellationToken);
        }

        /// <summary>
        /// Issue a <c>GET</c> request returning a string by following the <paramref name="link"/> from <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">An entity previously retrieved from the API.</param>
        /// <param name="link">The name of the outbound link template present in <paramref name="entity"/>'s <see cref="ILinked.Links"/> collection.</param>
        /// <param name="parameters">Named parameters to substitute into the link template, if required.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> supporting cancellation.</param>
        /// <returns>The response as a string.</returns>
        public Task<string> GetStringAsync(ILinked entity, string link, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            return HttpGetStringAsync(linkUri, cancellationToken);
        }

        /// <summary>
        /// Issue a <c>GET</c> request returning a list of <typeparamref name="TEntity"/> by following the <paramref name="link"/> from <paramref name="entity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type into which the response items should be deserialized.</typeparam>
        /// <param name="entity">An entity previously retrieved from the API.</param>
        /// <param name="link">The name of the outbound link template present in <paramref name="entity"/>'s <see cref="ILinked.Links"/> collection.</param>
        /// <param name="parameters">Named parameters to substitute into the link template, if required.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> supporting cancellation.</param>
        /// <returns>The response entities.</returns>
        public Task<List<TEntity>> ListAsync<TEntity>(ILinked entity, string link, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            return HttpGetAsync<List<TEntity>>(linkUri, cancellationToken);
        }

        /// <summary>
        /// Issue a <c>POST</c> request accepting a serialized <typeparamref name="TEntity"/> to a <paramref name="link"/> from <paramref name="entity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type that will be serialized into the request payload.</typeparam>
        /// <param name="entity">An entity previously retrieved from the API.</param>
        /// <param name="link">The name of the outbound link template present in <paramref name="entity"/>'s <see cref="ILinked.Links"/> collection.</param>
        /// <param name="parameters">Named parameters to substitute into the link template, if required.</param>
        /// <param name="content">The request content.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> supporting cancellation.</param>
        /// <returns>A task that signals request completion.</returns>
        public async Task PostAsync<TEntity>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Post, linkUri) { Content = MakeJsonContent(content) };
            var stream = await HttpSendAsync(request, cancellationToken).ConfigureAwait(false);
            using var reader = new StreamReader(stream);
            await reader.ReadToEndAsync();
        }

        /// <summary>
        /// Issue a <c>POST</c> request accepting a serialized <typeparamref name="TEntity"/> and returning a <typeparamref name="TResponse"/> by following <paramref name="link"/> from <paramref name="entity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type that will be serialized into the request payload.</typeparam>
        /// <typeparam name="TResponse">The entity type into which the response should be deserialized.</typeparam>
        /// <param name="entity">An entity previously retrieved from the API.</param>
        /// <param name="link">The name of the outbound link template present in <paramref name="entity"/>'s <see cref="ILinked.Links"/> collection.</param>
        /// <param name="parameters">Named parameters to substitute into the link template, if required.</param>
        /// <param name="content">The request content.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> supporting cancellation.</param>
        /// <returns>The response entity.</returns>
        public async Task<TResponse> PostAsync<TEntity, TResponse>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Post, linkUri) { Content = MakeJsonContent(content) };
            var stream = await HttpSendAsync(request, cancellationToken).ConfigureAwait(false);
            return _serializer.Deserialize<TResponse>(new JsonTextReader(new StreamReader(stream)));
        }

        /// <summary>
        /// Issue a <c>POST</c> request accepting a serialized <typeparamref name="TEntity"/> and returning a string by following <paramref name="link"/> from <paramref name="entity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type that will be serialized into the request payload.</typeparam>
        /// <param name="entity">An entity previously retrieved from the API.</param>
        /// <param name="link">The name of the outbound link template present in <paramref name="entity"/>'s <see cref="ILinked.Links"/> collection.</param>
        /// <param name="parameters">Named parameters to substitute into the link template, if required.</param>
        /// <param name="content">The request content.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> supporting cancellation.</param>
        /// <returns>The response string.</returns>
        public async Task<string> PostReadStringAsync<TEntity>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Post, linkUri) { Content = MakeJsonContent(content) };
            var stream = await HttpSendAsync(request, cancellationToken).ConfigureAwait(false);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        /// <summary>
        /// Issue a <c>POST</c> request accepting a serialized <typeparamref name="TEntity"/> and returning a stream by following <paramref name="link"/> from <paramref name="entity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type that will be serialized into the request payload.</typeparam>
        /// <param name="entity">An entity previously retrieved from the API.</param>
        /// <param name="link">The name of the outbound link template present in <paramref name="entity"/>'s <see cref="ILinked.Links"/> collection.</param>
        /// <param name="parameters">Named parameters to substitute into the link template, if required.</param>
        /// <param name="content">The request content.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> supporting cancellation.</param>
        /// <returns>The response stream.</returns>
        public async Task<Stream> PostReadStreamAsync<TEntity>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Post, linkUri) { Content = MakeJsonContent(content) };
            return await HttpSendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Issue a <c>PUT</c> request accepting a serialized <typeparamref name="TEntity"/> to a <paramref name="link"/> from <paramref name="entity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type that will be serialized into the request payload.</typeparam>
        /// <param name="entity">An entity previously retrieved from the API.</param>
        /// <param name="link">The name of the outbound link template present in <paramref name="entity"/>'s <see cref="ILinked.Links"/> collection.</param>
        /// <param name="parameters">Named parameters to substitute into the link template, if required.</param>
        /// <param name="content">The request content.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> supporting cancellation.</param>
        /// <returns>A task that signals request completion.</returns>
        public async Task PutAsync<TEntity>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Put, linkUri) { Content = MakeJsonContent(content) };
            var stream = await HttpSendAsync(request, cancellationToken).ConfigureAwait(false);
            using var reader = new StreamReader(stream);
            await reader.ReadToEndAsync();
        }

        /// <summary>
        /// Issue a <c>DELETE</c> request accepting a serialized <typeparamref name="TEntity"/> to a <paramref name="link"/> from <paramref name="entity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type that will be serialized into the request payload.</typeparam>
        /// <param name="entity">An entity previously retrieved from the API.</param>
        /// <param name="link">The name of the outbound link template present in <paramref name="entity"/>'s <see cref="ILinked.Links"/> collection.</param>
        /// <param name="parameters">Named parameters to substitute into the link template, if required.</param>
        /// <param name="content">The request content.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> supporting cancellation.</param>
        /// <returns>A task that signals request completion.</returns>
        public async Task DeleteAsync<TEntity>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Delete, linkUri) { Content = MakeJsonContent(content) };
            var stream = await HttpSendAsync(request, cancellationToken).ConfigureAwait(false);
            using var reader = new StreamReader(stream);
            await reader.ReadToEndAsync();
        }

        /// <summary>
        /// Issue a <c>DELETE</c> request accepting a serialized <typeparamref name="TEntity"/> to a <paramref name="link"/> from <paramref name="entity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type that will be serialized into the request payload.</typeparam>
        /// <typeparam name="TResponse">The entity type into which the response should be deserialized.</typeparam>
        /// <param name="entity">An entity previously retrieved from the API.</param>
        /// <param name="link">The name of the outbound link template present in <paramref name="entity"/>'s <see cref="ILinked.Links"/> collection.</param>
        /// <param name="parameters">Named parameters to substitute into the link template, if required.</param>
        /// <param name="content">The request content.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> supporting cancellation.</param>
        /// <returns>The response entity.</returns>
        public async Task<TResponse> DeleteAsync<TEntity, TResponse>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Delete, linkUri) { Content = MakeJsonContent(content) };
            var stream = await HttpSendAsync(request, cancellationToken).ConfigureAwait(false);
            return _serializer.Deserialize<TResponse>(new JsonTextReader(new StreamReader(stream)));
        }

        /// <summary>
        /// Connect to a websocket at the address specified by following <paramref name="link"/> from <paramref name="entity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the values received over the websocket.</typeparam>
        /// <param name="entity">An entity previously retrieved from the API.</param>
        /// <param name="link">The name of the outbound link template present in <paramref name="entity"/>'s <see cref="ILinked.Links"/> collection.</param>
        /// <param name="parameters">Named parameters to substitute into the link template, if required.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> supporting cancellation.</param>
        /// <returns>A stream of values from the websocket.</returns>
        public async Task<ObservableStream<TEntity>> StreamAsync<TEntity>(ILinked entity, string link, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            return await WebSocketStreamAsync(entity, link, parameters, reader => _serializer.Deserialize<TEntity>(new JsonTextReader(reader)), cancellationToken);
        }

        /// <summary>
        /// Connect to a websocket at the address specified by following <paramref name="link"/> from <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">An entity previously retrieved from the API.</param>
        /// <param name="link">The name of the outbound link template present in <paramref name="entity"/>'s <see cref="ILinked.Links"/> collection.</param>
        /// <param name="parameters">Named parameters to substitute into the link template, if required.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> supporting cancellation.</param>
        /// <returns>A stream of raw messages from the websocket.</returns>
        public async Task<ObservableStream<string>> StreamTextAsync(ILinked entity, string link, IDictionary<string, object> parameters = null, CancellationToken cancellationToken = default)
        {
            return await WebSocketStreamAsync(entity, link, parameters, reader => reader.ReadToEnd(), cancellationToken);
        }

        async Task<ObservableStream<T>> WebSocketStreamAsync<T>(ILinked entity, string link, IDictionary<string, object> parameters, Func<TextReader, T> deserialize, CancellationToken cancellationToken = default)
        {
            var linkUri = ResolveLink(entity, link, parameters);

            var socket = new ClientWebSocket();
            socket.Options.Cookies = _cookies;
            if (_apiKey != null)
                socket.Options.SetRequestHeader("X-Seq-ApiKey", _apiKey);

            await socket.ConnectAsync(new Uri(linkUri), cancellationToken);

            return new ObservableStream<T>(socket, deserialize);
        }

        async Task<T> HttpGetAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var stream = await HttpSendAsync(request, cancellationToken).ConfigureAwait(false);
            return _serializer.Deserialize<T>(new JsonTextReader(new StreamReader(stream)));
        }

        async Task<string> HttpGetStringAsync(string url, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var stream = await HttpSendAsync(request, cancellationToken).ConfigureAwait(false);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        async Task<Stream> HttpSendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
                return stream;

            Dictionary<string, object> payload = null;
            try
            {
                payload = _serializer.Deserialize<Dictionary<string, object>>(new JsonTextReader(new StreamReader(stream)));
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch { }

            if (payload != null && payload.TryGetValue("Error", out var error) && error != null)
                throw new SeqApiException($"{(int)response.StatusCode} - {error}", response.StatusCode);

            throw new SeqApiException($"The Seq request failed ({(int)response.StatusCode}/{response.StatusCode}).", response.StatusCode);
        }

        HttpContent MakeJsonContent(object content)
        {
            var json = new StringWriter();
            _serializer.Serialize(json, content);
            return new StringContent(json.ToString(), Encoding.UTF8, "application/json");
        }

        static string ResolveLink(ILinked entity, string link, IDictionary<string, object> parameters = null)
        {
            if (!entity.Links.TryGetValue(link, out var linkItem))
                throw new NotSupportedException($"The requested link `{link}` isn't available on entity `{entity}`.");

            return linkItem.GetUri(parameters);
        }

        /// <inheritdoc/>>
        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}