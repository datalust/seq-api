using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Tavis.UriTemplates;
using System.Threading;
using Seq.Api.Streams;
using System.Net.WebSockets;

namespace Seq.Api.Client
{
    public class SeqApiClient : IDisposable
    {
        readonly string _apiKey;

        // Future versions of Seq may not completely support v1 features, however
        // providing this as an Accept header will ensure what compatibility is available
        // can be utilised.
        const string SeqApiV4MediaType = "application/vnd.continuousit.seq.v4+json";

        readonly HttpClient _httpClient;
        readonly CookieContainer _cookies = new CookieContainer();
        readonly JsonSerializer _serializer = JsonSerializer.Create(
            new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter(), new LinkCollectionConverter() }
            });

        public SeqApiClient(string serverUrl, string apiKey = null)
        {
            ServerUrl = serverUrl ?? throw new ArgumentNullException(nameof(serverUrl));

            if (!string.IsNullOrEmpty(apiKey))
                _apiKey = apiKey;

            var handler = new HttpClientHandler { CookieContainer = _cookies, UseDefaultCredentials = true };

            var baseAddress = serverUrl;
            if (!baseAddress.EndsWith("/"))
                baseAddress += "/";

            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(baseAddress) };
        }

        public string ServerUrl { get; }

        public HttpClient HttpClient => _httpClient;

        public Task<RootEntity> GetRootAsync()
        {
            return HttpGetAsync<RootEntity>("api");
        }

        public Task<TEntity> GetAsync<TEntity>(ILinked entity, string link, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            return HttpGetAsync<TEntity>(linkUri);
        }

        public Task<string> GetStringAsync(ILinked entity, string link, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            return HttpGetStringAsync(linkUri);
        }

        public Task<List<TEntity>> ListAsync<TEntity>(ILinked entity, string link, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            return HttpGetAsync<List<TEntity>>(linkUri);
        }

        public async Task PostAsync<TEntity>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Post, linkUri) { Content = MakeJsonContent(content) };
            var stream = await HttpSendAsync(request).ConfigureAwait(false);
            new StreamReader(stream).ReadToEnd();
        }

        public async Task<TResponse> PostAsync<TEntity, TResponse>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Post, linkUri) { Content = MakeJsonContent(content) };
            var stream = await HttpSendAsync(request).ConfigureAwait(false);
            return _serializer.Deserialize<TResponse>(new JsonTextReader(new StreamReader(stream)));
        }

        public async Task<string> PostReadStringAsync<TEntity>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Post, linkUri) { Content = MakeJsonContent(content) };
            var stream = await HttpSendAsync(request).ConfigureAwait(false);
            return await new StreamReader(stream).ReadToEndAsync();
        }

        public async Task PutAsync<TEntity>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Put, linkUri) { Content = MakeJsonContent(content) };
            var stream = await HttpSendAsync(request).ConfigureAwait(false);
            new StreamReader(stream).ReadToEnd();
        }

        public async Task DeleteAsync<TEntity>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Delete, linkUri) { Content = MakeJsonContent(content) };
            var stream = await HttpSendAsync(request).ConfigureAwait(false);
            new StreamReader(stream).ReadToEnd();
        }

        public async Task<TResponse> DeleteAsync<TEntity, TResponse>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Delete, linkUri) { Content = MakeJsonContent(content) };
            var stream = await HttpSendAsync(request).ConfigureAwait(false);
            return _serializer.Deserialize<TResponse>(new JsonTextReader(new StreamReader(stream)));
        }

        public async Task<ObservableStream<TEntity>> StreamAsync<TEntity>(ILinked entity, string link, IDictionary<string, object> parameters = null)
        {
            return await WebSocketStreamAsync(entity, link, parameters, reader => _serializer.Deserialize<TEntity>(new JsonTextReader(reader)));
        }

        public async Task<ObservableStream<string>> StreamTextAsync(ILinked entity, string link, IDictionary<string, object> parameters = null)
        {
            return await WebSocketStreamAsync(entity, link, parameters, reader => reader.ReadToEnd());
        }

        async Task<ObservableStream<T>> WebSocketStreamAsync<T>(ILinked entity, string link, IDictionary<string, object> parameters, Func<TextReader, T> deserialize)
        {
            var linkUri = ResolveLink(entity, link, parameters);

            var socket = new ClientWebSocket();
            socket.Options.Cookies = _cookies;
            if (_apiKey != null)
                socket.Options.SetRequestHeader("X-Seq-ApiKey", _apiKey);

            await socket.ConnectAsync(new Uri(linkUri), CancellationToken.None);

            return new ObservableStream<T>(socket, deserialize);
        }

        async Task<T> HttpGetAsync<T>(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var stream = await HttpSendAsync(request).ConfigureAwait(false);
            return _serializer.Deserialize<T>(new JsonTextReader(new StreamReader(stream)));
        }

        async Task<string> HttpGetStringAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var stream = await HttpSendAsync(request).ConfigureAwait(false);
            return await new StreamReader(stream).ReadToEndAsync();
        }

        async Task<Stream> HttpSendAsync(HttpRequestMessage request)
        {
            if (_apiKey != null)
                request.Headers.Add("X-Seq-ApiKey", _apiKey);

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(SeqApiV4MediaType));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
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

            object error;
            if (payload != null && payload.TryGetValue("Error", out error) && error != null)
                throw new SeqApiException($"{(int)response.StatusCode} - {error}");

            throw new SeqApiException($"The Seq request failed ({(int)response.StatusCode}).");
        }

        HttpContent MakeJsonContent(object content)
        {
            var json = new StringWriter();
            _serializer.Serialize(json, content);
            return new StringContent(json.ToString(), Encoding.UTF8, "application/json");
        }

        static string ResolveLink(ILinked entity, string link, IDictionary<string, object> parameters = null)
        {
            Link linkItem;
            if (!entity.Links.TryGetValue(link, out linkItem))
                throw new NotSupportedException("The requested link isn't available.");

            var expression = linkItem.GetUri();
            var template = new UriTemplate(expression);
            if (parameters != null)
            {
                var missing = parameters.Select(p => p.Key).Except(template.GetParameterNames()).ToArray();
                if (missing.Any())
                    throw new ArgumentException("The URI template '" + expression + "' does not contain parameter: " + string.Join(",", missing));

                foreach (var parameter in parameters)
                {
                    var value = parameter.Value is DateTime
                        ? ((DateTime) parameter.Value).ToString("O")
                        : parameter.Value;

                    template.SetParameter(parameter.Key, value);
                }
            }

            return template.Resolve();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}