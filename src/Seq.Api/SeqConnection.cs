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
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Client;
using Seq.Api.Model;
using Seq.Api.Model.Root;
using Seq.Api.ResourceGroups;

namespace Seq.Api
{
    /// <summary>
    /// Exposes high-level (typed) interactions with the Seq API through various resource groups.
    /// </summary>
    public class SeqConnection : ILoadResourceGroup, IDisposable
    {
        readonly object _sync = new object();
        readonly Dictionary<string, Task<ResourceGroup>> _resourceGroups = new Dictionary<string, Task<ResourceGroup>>();
        Task<RootEntity> _root;

        /// <summary>
        /// Construct a <see cref="SeqConnection"/>.
        /// </summary>
        /// <param name="serverUrl">The base URL of the Seq server.</param>
        /// <param name="apiKey">An API key to use when making requests to the server, if required.</param>
        /// <param name="useDefaultCredentials">Whether default credentials will be sent with HTTP requests; the default is <c>true</c>.</param>
        [Obsolete("Prefer `SeqConnection(serverUrl, apiKey, createHttpMessageHandler)` instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public SeqConnection(string serverUrl, string apiKey, bool useDefaultCredentials)
        {
            if (serverUrl == null) throw new ArgumentNullException(nameof(serverUrl));
            Client = new SeqApiClient(serverUrl, apiKey, useDefaultCredentials);
        }

        /// <summary>
        /// Construct a <see cref="SeqConnection"/>.
        /// </summary>
        /// <param name="serverUrl">The base URL of the Seq server.</param>
        /// <param name="apiKey">An API key to use when making requests to the server, if required.</param>
        /// <param name="configureHttpClientHandler">An optional callback to configure the <see cref="HttpClientHandler"/> used when making HTTP requests
        /// to the Seq API.</param>
        [Obsolete("Prefer `SeqConnection(serverUrl, apiKey, createHttpMessageHandler)` instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public SeqConnection(string serverUrl, string apiKey, Action<HttpClientHandler> configureHttpClientHandler)
        {
            if (serverUrl == null) throw new ArgumentNullException(nameof(serverUrl));
            Client = new SeqApiClient(serverUrl, apiKey, configureHttpClientHandler);
        }

        /// <summary>
        /// Construct a <see cref="SeqConnection"/>.
        /// </summary>
        /// <param name="serverUrl">The base URL of the Seq server.</param>
        /// <param name="apiKey">An API key to use when making requests to the server, if required.</param>
        /// <param name="createHttpMessageHandler">An optional callback to construct the HTTP message handler used when making requests
        /// to the Seq API. The callback receives a <see cref="CookieContainer"/> that is shared with WebSocket requests made by the client.</param>
        public SeqConnection(string serverUrl, string apiKey = null, Func<CookieContainer, HttpMessageHandler> createHttpMessageHandler = null)
        {
            if (serverUrl == null) throw new ArgumentNullException(nameof(serverUrl));
            Client = new SeqApiClient(serverUrl, apiKey, createHttpMessageHandler);
        }
        
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Dispose()
        {
            Client.Dispose();
        }

        /// <summary>
        /// Access the lower-level <see cref="SeqApiClient"/> that can be used for resource-oriented navigation through
        /// the HTTP API.
        /// </summary>
        public SeqApiClient Client { get; }
        
        /// <summary>
        /// Create and manage alerts.
        /// </summary>
        public AlertsResourceGroup Alerts => new AlertsResourceGroup(this);

        /// <summary>
        /// List and administratively remove active alerts. To create/edit/remove alerts in normal
        /// circumstances, use <see cref="Dashboards"/>.
        /// </summary>
        public AlertStateResourceGroup AlertState => new AlertStateResourceGroup(this);

        /// <summary>
        /// Perform operations on API keys.
        /// </summary>
        public ApiKeysResourceGroup ApiKeys => new ApiKeysResourceGroup(this);

        /// <summary>
        /// Perform operations on Seq app instances.
        /// </summary>
        public AppInstancesResourceGroup AppInstances => new AppInstancesResourceGroup(this);

        /// <summary>
        /// Perform operations on Seq app packages.
        /// </summary>
        public AppsResourceGroup Apps => new AppsResourceGroup(this);

        /// <summary>
        /// Perform operations on backups.
        /// </summary>
        public BackupsResourceGroup Backups => new BackupsResourceGroup(this);
        
        /// <summary>
        /// Perform operations on the Seq cluster.
        /// </summary>
        public ClusterNodesResourceGroup ClusterNodes => new ClusterNodesResourceGroup(this);
        
        /// <summary>
        /// Perform operations on dashboards.
        /// </summary>
        public DashboardsResourceGroup Dashboards => new DashboardsResourceGroup(this);

        /// <summary>
        /// Execute SQL-style queries against the API.
        /// </summary>
        public DataResourceGroup Data => new DataResourceGroup(this);

        /// <summary>
        /// Access server diagnostics.
        /// </summary>
        public DiagnosticsResourceGroup Diagnostics => new DiagnosticsResourceGroup(this);

        /// <summary>
        /// Read and subscribe to events from the event store.
        /// </summary>
        public EventsResourceGroup Events => new EventsResourceGroup(this);

        /// <summary>
        /// Perform operations on queries and filter expressions.
        /// </summary>
        public ExpressionsResourceGroup Expressions => new ExpressionsResourceGroup(this);

        /// <summary>
        /// Perform operations on NuGet feeds.
        /// </summary>
        public FeedsResourceGroup Feeds => new FeedsResourceGroup(this);

        /// <summary>
        /// Perform operations on the Seq license certificate.
        /// </summary>
        public LicensesResourceGroup Licenses => new LicensesResourceGroup(this);

        /// <summary>
        /// Perform operations on permalinked events.
        /// </summary>
        public PermalinksResourceGroup Permalinks => new PermalinksResourceGroup(this);

        /// <summary>
        /// Perform operations on retention policies.
        /// </summary>
        public RetentionPoliciesResourceGroup RetentionPolicies => new RetentionPoliciesResourceGroup(this);
        
        /// <summary>
        /// Perform operations on user roles.
        /// </summary>
        public RolesResourceGroup Roles => new RolesResourceGroup(this);

        /// <summary>
        /// Perform operations on tasks running in the Seq server.
        /// </summary>
        public RunningTasksResourceGroup RunningTasks => new RunningTasksResourceGroup(this);

        /// <summary>
        /// Perform operations on system settings.
        /// </summary>
        public SettingsResourceGroup Settings => new SettingsResourceGroup(this);

        /// <summary>
        /// Perform operations on signals.
        /// </summary>
        public SignalsResourceGroup Signals => new SignalsResourceGroup(this);

        /// <summary>
        /// Perform operations on saved SQL queries.
        /// </summary>
        public SqlQueriesResourceGroup SqlQueries => new SqlQueriesResourceGroup(this);

        /// <summary>
        /// Perform operations on known available Seq versions.
        /// </summary>
        public UpdatesResourceGroup Updates => new UpdatesResourceGroup(this);

        /// <summary>
        /// Perform operations on users.
        /// </summary>
        public UsersResourceGroup Users => new UsersResourceGroup(this);

        /// <summary>
        /// Perform operations on workspaces.
        /// </summary>
        public WorkspacesResourceGroup Workspaces => new WorkspacesResourceGroup(this);

        /// <summary>
        /// Check that the Seq API is available. If the initial attempt fails (i.e. the server is starting up),
        /// the method will try every 100 milliseconds until <paramref name="timeout"/> is reached.
        /// </summary>
        /// <param name="timeout">The maximum amount of time to retry until giving up.</param>
        /// <returns>A task that will complete if the API could be reached, or fault otherwise.</returns>
        public async Task EnsureConnectedAsync(TimeSpan timeout)
        {
            var started = DateTime.UtcNow;
            // Fractional milliseconds are lost here, but that's fine.
            var wait = TimeSpan.FromMilliseconds(Math.Min(100, timeout.TotalMilliseconds));
            var deadline = started.Add(timeout);
            while (!await ConnectAsync(DateTime.UtcNow > deadline))
            {
                await Task.Delay(wait);
            }
        }

        async Task<bool> ConnectAsync(bool throwOnFailure)
        {
            HttpStatusCode statusCode;

            try
            {
                statusCode = (await Client.HttpClient.GetAsync("api")).StatusCode;
            }
            catch
            {
                if (throwOnFailure)
                    throw;

                return false;
            }

            if (statusCode == HttpStatusCode.OK)
                return true;

            if (!throwOnFailure)
                return false;

            throw new SeqApiException($"Could not connect to the Seq API endpoint: {(int)statusCode}/{statusCode}.", statusCode);
        }
        
        async Task<ResourceGroup> ILoadResourceGroup.LoadResourceGroupAsync(string name, CancellationToken cancellationToken)
        {
            Task<RootEntity> loadRoot;
            lock (_sync)
            {
                if (_root == null || _root.IsFaulted || _root.IsCanceled)
                    _root = Client.GetRootAsync(cancellationToken);

                loadRoot = _root;
            }

            var rootEntity = await loadRoot.ConfigureAwait(false);

            Task<ResourceGroup> loadGroup;
            lock (_sync)
            {
                // ReSharper disable once InvertIf
                if (!_resourceGroups.TryGetValue(name, out loadGroup) || loadGroup.IsFaulted || loadGroup.IsCanceled)
                {
                    loadGroup = GetResourceGroup(rootEntity, name, cancellationToken);
                    _resourceGroups.Add(name, loadGroup);
                }
            }

            return await loadGroup.ConfigureAwait(false);
        }

        async Task<ResourceGroup> GetResourceGroup(RootEntity root, string name, CancellationToken cancellationToken)
        {
            return await Client.GetAsync<ResourceGroup>(root, name + "Resources", cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
