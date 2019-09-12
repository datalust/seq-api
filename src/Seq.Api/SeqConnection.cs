using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Client;
using Seq.Api.Model;
using Seq.Api.Model.Root;
using Seq.Api.ResourceGroups;

namespace Seq.Api
{
    public class SeqConnection : ISeqConnection
    {
        readonly object _sync = new object();
        readonly Dictionary<string, Task<ResourceGroup>> _resourceGroups = new Dictionary<string, Task<ResourceGroup>>();
        Task<RootEntity> _root;

        public SeqConnection(string serverUrl, string apiKey = null, bool useDefaultCredentials = true)
        {
            if (serverUrl == null) throw new ArgumentNullException(nameof(serverUrl));
            Client = new SeqApiClient(serverUrl, apiKey, useDefaultCredentials);
        }
        
        public SeqApiClient Client { get; }

        public ApiKeysResourceGroup ApiKeys => new ApiKeysResourceGroup(this);

        public AppInstancesResourceGroup AppInstances => new AppInstancesResourceGroup(this);

        public AppsResourceGroup Apps => new AppsResourceGroup(this);

        public BackupsResourceGroup Backups => new BackupsResourceGroup(this);

        public DashboardsResourceGroup Dashboards => new DashboardsResourceGroup(this);

        public DataResourceGroup Data => new DataResourceGroup(this);

        public DiagnosticsResourceGroup Diagnostics => new DiagnosticsResourceGroup(this);

        public EventsResourceGroup Events => new EventsResourceGroup(this);

        public ExpressionsResourceGroup Expressions => new ExpressionsResourceGroup(this);

        public FeedsResourceGroup Feeds => new FeedsResourceGroup(this);

        public LicensesResourceGroup Licenses => new LicensesResourceGroup(this);

        public PermalinksResourceGroup Permalinks => new PermalinksResourceGroup(this);

        public RetentionPoliciesResourceGroup RetentionPolicies => new RetentionPoliciesResourceGroup(this);

        public SettingsResourceGroup Settings => new SettingsResourceGroup(this);

        public SignalsResourceGroup Signals => new SignalsResourceGroup(this);

        public SqlQueriesResourceGroup SqlQueries => new SqlQueriesResourceGroup(this);

        public UpdatesResourceGroup Updates => new UpdatesResourceGroup(this);

        public UsersResourceGroup Users => new UsersResourceGroup(this);

        public WorkspacesResourceGroup Workspaces => new WorkspacesResourceGroup(this);

        public async Task EnsureConnected(TimeSpan timeout)
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

            throw new WebException($"Could not connect to the Seq API endpoint: {(int)statusCode}/{statusCode}.");
        }

        public async Task<ResourceGroup> LoadResourceGroupAsync(string name, CancellationToken cancellationToken = default)
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

        async Task<ResourceGroup> GetResourceGroup(RootEntity root, string name, CancellationToken cancellationToken = default)
        {
            return await Client.GetAsync<ResourceGroup>(root, name + "Resources", cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
