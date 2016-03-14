using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Seq.Api.Client;
using Seq.Api.Model;
using Seq.Api.Model.Root;
using Seq.Api.ResourceGroups;

namespace Seq.Api
{
    public class SeqConnection : ISeqConnection
    {
        readonly SeqApiClient _client;
        readonly ConcurrentDictionary<string, Task<ResourceGroup>> _resourceGroups = new ConcurrentDictionary<string, Task<ResourceGroup>>();
        readonly Lazy<Task<RootEntity>> _root;

        public SeqConnection(string serverUrl, string apiKey = null)
        {
            if (serverUrl == null) throw new ArgumentNullException(nameof(serverUrl));
            _client = new SeqApiClient(serverUrl, apiKey);
            
            _root = new Lazy<Task<RootEntity>>(() => _client.GetRootAsync());

        }

        public ApiKeysResourceGroup ApiKeys => new ApiKeysResourceGroup(this);

        public AppInstancesResourceGroup AppInstances => new AppInstancesResourceGroup(this);

        public AppsResourceGroup Apps => new AppsResourceGroup(this);

        public BackupsResourceGroup Backups => new BackupsResourceGroup(this);

        public DataResourceGroup Data => new DataResourceGroup(this);

        public DiagnosticsResourceGroup Diagnositcs => new DiagnosticsResourceGroup(this);

        public EventsResourceGroup Events => new EventsResourceGroup(this);

        public ExpressionsResourceGroup Expressions => new ExpressionsResourceGroup(this);

        public FeedsResourceGroup Feeds => new FeedsResourceGroup(this);

        public LicensesResourceGroup Licenses => new LicensesResourceGroup(this);

        public PinsResourceGroup Pins => new PinsResourceGroup(this);

        public RetentionPoliciesResourceGroup RetentionPolicies => new RetentionPoliciesResourceGroup(this);

        public SettingsResourceGroup Settings => new SettingsResourceGroup(this);

        public SignalsResourceGroup Signals => new SignalsResourceGroup(this);

        public UpdatesResourceGroup Updates => new UpdatesResourceGroup(this);

        public UsersResourceGroup Users => new UsersResourceGroup(this);

        public WatchesResourceGroup Watches => new WatchesResourceGroup(this);

        public async Task<ResourceGroup> LoadResourceGroupAsync(string name)
        {
            return await _resourceGroups.GetOrAdd(name, ResourceGroupFactory).ConfigureAwait(false);
        }

        private async Task<ResourceGroup> ResourceGroupFactory(string name)
        {
            return await _client.GetAsync<ResourceGroup>(await _root.Value, name + "Resources").ConfigureAwait(false);
        }

        public SeqApiClient Client => _client;
    }
}
