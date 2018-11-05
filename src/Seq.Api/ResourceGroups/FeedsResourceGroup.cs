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

        public async Task<NuGetFeedEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<NuGetFeedEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<NuGetFeedEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<NuGetFeedEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<NuGetFeedEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<NuGetFeedEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<NuGetFeedEntity> AddAsync(NuGetFeedEntity entity, CancellationToken cancellationToken = default)
        {
            return await Client.PostAsync<NuGetFeedEntity, NuGetFeedEntity>(entity, "Create", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task RemoveAsync(NuGetFeedEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(NuGetFeedEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}