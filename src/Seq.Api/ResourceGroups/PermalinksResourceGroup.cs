using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Permalinks;

namespace Seq.Api.ResourceGroups
{
    public class PermalinksResourceGroup : ApiResourceGroup
    {
        internal PermalinksResourceGroup(ISeqConnection connection)
            : base("Permalinks", connection)
        {
        }

        public async Task<PermalinkEntity> FindAsync(
            string id,
            bool includeEvent = false,
            bool renderEvent = false,
            CancellationToken token = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var parameters = new Dictionary<string, object>
            {
                {"id", id},
                {"includeEvent", includeEvent},
                {"renderEvent", renderEvent}
            };
            return await GroupGetAsync<PermalinkEntity>("Item", parameters, token).ConfigureAwait(false);
        }

        public async Task<List<PermalinkEntity>> ListAsync(
            bool includeEvent = false,
            bool renderEvent = false,
            CancellationToken token = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"includeEvent", includeEvent},
                {"renderEvent", renderEvent}
            };

            return await GroupListAsync<PermalinkEntity>("Items", parameters, token).ConfigureAwait(false);
        }

        public async Task<PermalinkEntity> TemplateAsync(CancellationToken token = default)
        {
            return await GroupGetAsync<PermalinkEntity>("Template", token: token).ConfigureAwait(false);
        }

        public async Task<PermalinkEntity> AddAsync(PermalinkEntity entity, CancellationToken token = default)
        {
            return await GroupCreateAsync<PermalinkEntity, PermalinkEntity>(entity, token: token).ConfigureAwait(false);
        }

        public async Task RemoveAsync(PermalinkEntity entity, CancellationToken token = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }

        public async Task UpdateAsync(PermalinkEntity entity, CancellationToken token = default)
        {
            await Client.PutAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }
    }
}
