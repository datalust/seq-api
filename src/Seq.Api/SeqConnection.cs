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
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Seq.Api.Client;
using Seq.Api.Model;
using Seq.Api.Model.Root;
using Seq.Api.Model.Shared;
using Seq.Api.ResourceGroups;

namespace Seq.Api
{
    /// <summary>
    /// Exposes high-level (typed) interactions with the Seq API through various resource groups.
    /// </summary>
    public class SeqConnection : ILoadResourceGroup, IDisposable
    {
        readonly object _sync = new();
        readonly Dictionary<string, Task<ResourceGroup>> _resourceGroups = new();
        Task<RootEntity> _root;

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
        public AlertsResourceGroup Alerts => new(this);

        /// <summary>
        /// List and administratively remove active alerts. To create/edit/remove alerts in normal
        /// circumstances, use <see cref="Dashboards"/>.
        /// </summary>
        public AlertStateResourceGroup AlertState => new(this);

        /// <summary>
        /// Perform operations on API keys.
        /// </summary>
        public ApiKeysResourceGroup ApiKeys => new(this);

        /// <summary>
        /// Perform operations on Seq app instances.
        /// </summary>
        public AppInstancesResourceGroup AppInstances => new(this);

        /// <summary>
        /// Perform operations on Seq app packages.
        /// </summary>
        public AppsResourceGroup Apps => new(this);

        /// <summary>
        /// Perform operations on backups.
        /// </summary>
        public BackupsResourceGroup Backups => new(this);

        /// <summary>
        /// Perform operations on Seq cluster nodes.
        /// </summary>
        public ClusterResourceGroup Cluster => new(this);
        
        /// <summary>
        /// Perform operations on dashboards.
        /// </summary>
        public DashboardsResourceGroup Dashboards => new(this);

        /// <summary>
        /// Execute SQL-style queries against the API.
        /// </summary>
        public DataResourceGroup Data => new(this);

        /// <summary>
        /// Access server diagnostics.
        /// </summary>
        public DiagnosticsResourceGroup Diagnostics => new(this);

        /// <summary>
        /// Read and subscribe to events from the event store.
        /// </summary>
        public EventsResourceGroup Events => new(this);

        /// <summary>
        /// Perform operations on queries and filter expressions.
        /// </summary>
        public ExpressionsResourceGroup Expressions => new(this);
        
        /// <summary>
        /// Perform operations on expression indexes.
        /// </summary>
        public ExpressionIndexesResourceGroup ExpressionIndexes => new(this);

        /// <summary>
        /// Perform operations on NuGet feeds.
        /// </summary>
        public FeedsResourceGroup Feeds => new(this);

        /// <summary>
        /// Statistics about indexes.
        /// </summary>
        public IndexesResourceGroup Indexes => new(this);

        /// <summary>
        /// Perform operations on the Seq license certificate.
        /// </summary>
        public LicensesResourceGroup Licenses => new(this);

        /// <summary>
        /// Perform operations on permalinked events.
        /// </summary>
        public PermalinksResourceGroup Permalinks => new(this);

        /// <summary>
        /// Perform operations on retention policies.
        /// </summary>
        public RetentionPoliciesResourceGroup RetentionPolicies => new(this);
        
        /// <summary>
        /// Perform operations on user roles.
        /// </summary>
        public RolesResourceGroup Roles => new(this);

        /// <summary>
        /// Perform operations on tasks running in the Seq server.
        /// </summary>
        public RunningTasksResourceGroup RunningTasks => new(this);

        /// <summary>
        /// Perform operations on system settings.
        /// </summary>
        public SettingsResourceGroup Settings => new(this);

        /// <summary>
        /// Perform operations on signals.
        /// </summary>
        public SignalsResourceGroup Signals => new(this);

        /// <summary>
        /// Perform operations on saved SQL queries.
        /// </summary>
        public SqlQueriesResourceGroup SqlQueries => new(this);

        /// <summary>
        /// Perform operations on known available Seq versions.
        /// </summary>
        public UpdatesResourceGroup Updates => new(this);

        /// <summary>
        /// Perform operations on users.
        /// </summary>
        public UsersResourceGroup Users => new(this);

        /// <summary>
        /// Perform operations on workspaces.
        /// </summary>
        public WorkspacesResourceGroup Workspaces => new(this);

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
            HttpResponseMessage response;

            try
            {
                response = await Client.HttpClient.GetAsync("api");
            }
            catch
            {
                if (throwOnFailure)
                    throw;

                return false;
            }

            var statusCode = response.StatusCode;
            if (statusCode == HttpStatusCode.OK)
                return true;

            if (!throwOnFailure)
                return false;
            
            ErrorPart error = null;
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                error = JsonConvert.DeserializeObject<ErrorPart>(content);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch { }

            var exceptionMessage = $"Could not connect to the Seq API endpoint ({(int)statusCode}/{statusCode}).";
            if (error?.Error != null)
                exceptionMessage += $" {error.Error}";

            throw new SeqApiException(exceptionMessage, statusCode);
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
