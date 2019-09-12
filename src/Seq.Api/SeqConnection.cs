using System;
using System.Collections.Concurrent;
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
        readonly ConcurrentDictionary<string, Task<ResourceGroup>> _resourceGroups = new ConcurrentDictionary<string, Task<ResourceGroup>>();
        readonly Lazy<Task<RootEntity>> _root;

        public SeqConnection(string serverUrl, string apiKey = null, bool useDefaultCredentials = true)
        {
            if (serverUrl == null) throw new ArgumentNullException(nameof(serverUrl));
            Client = new SeqApiClient(serverUrl, apiKey, useDefaultCredentials);

            _root = new Lazy<Task<RootEntity>>(() => Client.GetRootAsync());
        }

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

        public Task<ResourceGroup> LoadResourceGroupAsync(string name, CancellationToken cancellationToken = default)
        {
            // Initially, we want to put an incomplete task into the cache so that any concurrent attempts to load the
            // same resource group will wait on the same pending call.
            var cached = _resourceGroups.GetOrAdd(name, s => ResourceGroupFactory(s, cancellationToken));

            if (!cached.IsFaulted && !cached.IsCanceled)
                return cached;

            // If the cached task failed (ideally a rare situation), clobber it and return a new task (less worried about
            // overlapping/concurrent calls on this path).
            return _resourceGroups.AddOrUpdate(name,
                s => ResourceGroupFactory(s, cancellationToken),
                (s, _) => ResourceGroupFactory(s, cancellationToken));
        }

        async Task<ResourceGroup> ResourceGroupFactory(string name, CancellationToken cancellationToken = default)
        {
            return await Client.GetAsync<ResourceGroup>(await _root.Value, name + "Resources", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public SeqApiClient Client { get; }
    }
}
