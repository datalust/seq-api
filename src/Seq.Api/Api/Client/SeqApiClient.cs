using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Seq.Api.Model;
using Seq.Api.Serialization;
using Tavis.UriTemplates;

namespace Seq.Api.Client
{
    public class SeqApiClient : IDisposable
    {
        readonly string _serverUrl;

        readonly HttpClient _httpClient;
        readonly IDictionary<string, ResourceGroup> _resourceGroups = new Dictionary<string, ResourceGroup>();
        readonly JsonSerializer _serializer = JsonSerializer.Create(
            new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter(), new LinkCollectionConverter() }
            });

        public SeqApiClient(string serverUrl)
        {
            if (serverUrl == null) throw new ArgumentNullException("serverUrl");

            _serverUrl = serverUrl;

            var handler = new HttpClientHandler { CookieContainer = new CookieContainer() };

            var baseAddress = serverUrl;
            if (!baseAddress.EndsWith("/"))
                baseAddress += "/";

            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(baseAddress) };
        }

        public string ServerUrl
        {
            get { return _serverUrl; }
        }

        public string ApiKey { get; set; }

        public async Task<ResourceGroup> GetResourceGroup(string collection)
        {
            ResourceGroup group;
            if (!_resourceGroups.TryGetValue(collection, out group))
            {
                var resourcesUrl = string.Format("api/{0}/resources", collection);

                group = await HttpGet<ResourceGroup>(resourcesUrl);
                _resourceGroups.Add(collection, group);
            }
            return group;
        }

        public Task<TEntity> Get<TEntity>(ILinked entity, string link, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            return HttpGet<TEntity>(linkUri);
        }

        public Task<List<TEntity>> List<TEntity>(ILinked entity, string link, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            return HttpGet<List<TEntity>>(linkUri);
        }

        public async Task Post<TEntity>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Post, linkUri);
            var stream = await HttpSend(request);
            new StreamReader(stream).ReadToEnd();
        }

        public async Task<TResponse> Post<TEntity, TResponse>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Post, linkUri);
            var stream = await HttpSend(request);
            return _serializer.Deserialize<TResponse>(new JsonTextReader(new StreamReader(stream)));
        }

        public async Task Put<TEntity>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Put, linkUri);
            var stream = await HttpSend(request);
            new StreamReader(stream).ReadToEnd();
        }

        public async Task Delete<TEntity>(ILinked entity, string link, TEntity content, IDictionary<string, object> parameters = null)
        {
            var linkUri = ResolveLink(entity, link, parameters);
            var request = new HttpRequestMessage(HttpMethod.Delete, linkUri);
            var stream = await HttpSend(request);
            new StreamReader(stream).ReadToEnd();
        }

        async Task<T> HttpGet<T>(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var stream = await HttpSend(request);
            return _serializer.Deserialize<T>(new JsonTextReader(new StreamReader(stream)));
        }

        async Task<Stream> HttpSend(HttpRequestMessage request)
        {
            if (!string.IsNullOrEmpty(ApiKey))
                request.Headers.Add("X-Seq-ApiKey", ApiKey);
            var response = await _httpClient.SendAsync(request);
            var stream = await response.Content.ReadAsStreamAsync();
            return stream;
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
                    template.SetParameter(parameter.Key, parameter.Value);
            }

            return template.Resolve();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}