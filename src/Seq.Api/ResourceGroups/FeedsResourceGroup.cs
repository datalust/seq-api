using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Feeds;

namespace Seq.Api.ResourceGroups
{
    public class FeedsResourceGroup : ApiResourceGroup
    {
        internal FeedsResourceGroup(ISeqConnection connection)
            : base("Feeds", connection)
        {
        }

        public async Task<NuGetFeedEntity> FindAsync(string id, CancellationToken token = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<NuGetFeedEntity>("Item", new Dictionary<string, object> { { "id", id } }, token).ConfigureAwait(false);
        }

        public async Task<List<NuGetFeedEntity>> ListAsync(CancellationToken token = default)
        {
            return await GroupListAsync<NuGetFeedEntity>("Items", token: token).ConfigureAwait(false);
        }

        public async Task<NuGetFeedEntity> TemplateAsync(CancellationToken token = default)
        {
            return await GroupGetAsync<NuGetFeedEntity>("Template", token: token).ConfigureAwait(false);
        }

        public async Task<NuGetFeedEntity> AddAsync(NuGetFeedEntity entity, CancellationToken token = default)
        {
            return await Client.PostAsync<NuGetFeedEntity, NuGetFeedEntity>(entity, "Create", entity, token: token).ConfigureAwait(false);
        }

        public async Task RemoveAsync(NuGetFeedEntity entity, CancellationToken token = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }

        public async Task UpdateAsync(NuGetFeedEntity entity, CancellationToken token = default)
        {
            await Client.PutAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }
    }
}