using System;
using System.Collections.Generic;
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
        readonly Dictionary<string, ResourceGroup> _resourceGroups = new Dictionary<string, ResourceGroup>();
        RootEntity _root;

        public SeqConnection(string serverUrl, string apiKey = null)
        {
            if (serverUrl == null) throw new ArgumentNullException("serverUrl");
            _client = new SeqApiClient(serverUrl, apiKey);
        }

        public ApiKeysResourceGroup ApiKeys
        {
            get { return new ApiKeysResourceGroup(this); }
        }

        public AppInstancesResourceGroup AppInstances
        {
            get { return new AppInstancesResourceGroup(this); }
        }

        public AppsResourceGroup Apps
        {
            get { return new AppsResourceGroup(this); }
        }

        public EventsResourceGroup Events
        {
            get { return new EventsResourceGroup(this); }
        }

        public ExpressionsResourceGroup Expressions
        {
            get { return new ExpressionsResourceGroup(this); }
        }

        public FeedsResourceGroup Feeds
        {
            get { return new FeedsResourceGroup(this); }
        }

        public LicensesResourceGroup Licenses
        {
            get { return new LicensesResourceGroup(this); }
        }

        public MetricsResourceGroup Metrics
        {
            get { return new MetricsResourceGroup(this); }
        }

        public PinsResourceGroup Pins
        {
            get { return new PinsResourceGroup(this); }
        }

        public RetentionPoliciesResourceGroup RetentionPolicies
        {
            get { return new RetentionPoliciesResourceGroup(this); }
        }

        public SettingsResourceGroup Settings
        {
            get { return new SettingsResourceGroup(this); }
        }

        public SignalsResourceGroup Signals
        {
            get { return new SignalsResourceGroup(this); }
        }

        public UpdatesResourceGroup Updates
        {
            get {  return new UpdatesResourceGroup(this); }
        }

        public UsersResourceGroup Users
        {
            get { return new UsersResourceGroup(this); }
        }

        public WatchesResourceGroup Watches
        {
            get { return new WatchesResourceGroup(this); }
        }

        public async Task<ResourceGroup> LoadResourceGroupAsync(string name)
        {
            ResourceGroup loaded;
            if (_resourceGroups.TryGetValue(name, out loaded))
                return loaded;

            if (_root == null)
                _root = await _client.GetRootAsync();

            loaded = await _client.GetAsync<ResourceGroup>(_root, name + "Resources");
            _resourceGroups.Add(name, loaded);
            return loaded;
        }

        public SeqApiClient Client { get { return _client; } }
    }
}
