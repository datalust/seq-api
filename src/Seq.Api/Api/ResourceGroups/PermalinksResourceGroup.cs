using System;
using System.Collections.Generic;
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
            bool includeUser = false)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var parameters = new Dictionary<string, object>
            {
                {"id", id},
                {"includeEvent", includeEvent},
                {"renderEvent", renderEvent},
                {"includeUser", includeUser}
            };
            return await GroupGetAsync<PermalinkEntity>("Item", parameters).ConfigureAwait(false);
        }

        public async Task<List<PermalinkEntity>> ListAsync(
            bool includeEvent = false, 
            bool renderEvent = false, 
            bool includeUser = false)
        {
            var parameters = new Dictionary<string, object>
            {
                {"includeEvent", includeEvent},
                {"renderEvent", renderEvent},
                {"includeUser", includeUser}
            };

            return await GroupListAsync<PermalinkEntity>("Items", parameters).ConfigureAwait(false);
        }

        public async Task<PermalinkEntity> TemplateAsync()
        {
            return await GroupGetAsync<PermalinkEntity>("Template").ConfigureAwait(false);
        }

        public async Task<PermalinkEntity> AddAsync(PermalinkEntity entity)
        {
            return await Client.PostAsync<PermalinkEntity, PermalinkEntity>(entity, "Create", entity).ConfigureAwait(false);
        }

        public async Task RemoveAsync(PermalinkEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity).ConfigureAwait(false);
        }

        public async Task UpdateAsync(PermalinkEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity).ConfigureAwait(false);
        }
    }
}