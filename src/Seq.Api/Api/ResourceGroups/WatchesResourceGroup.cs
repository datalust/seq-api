using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Watches;

namespace Seq.Api.ResourceGroups
{
    public class WatchesResourceGroup : ApiResourceGroup
    {
        internal WatchesResourceGroup(ISeqConnection connection)
            : base("Watches", connection)
        {
        }

        public async Task<WatchEntity> FindAsync(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<WatchEntity>("Item", new Dictionary<string, object> { { "id", id } }).ConfigureAwait(false);
        }

        public async Task<List<WatchEntity>> ListAsync(string ownerId = null)
        {
            var parameters = new Dictionary<string, object>();
            if (ownerId != null)
                parameters.Add("ownerId", ownerId);
            return await GroupListAsync<WatchEntity>("Items", parameters).ConfigureAwait(false);
        }

        public async Task<WatchEntity> TemplateAsync()
        {
            return await GroupGetAsync<WatchEntity>("Template").ConfigureAwait(false);
        }

        public async Task<WatchEntity> AddAsync(WatchEntity entity)
        {
            return await Client.PostAsync<WatchEntity, WatchEntity>(entity, "Create", entity).ConfigureAwait(false);
        }

        public async Task RemoveAsync(WatchEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity).ConfigureAwait(false);
        }

        public async Task UpdateAsync(WatchEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity).ConfigureAwait(false);
        }
    }
}