using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Pins;

namespace Seq.Api.ResourceGroups
{
    public class PinsResourceGroup : ApiResourceGroup
    {
        internal PinsResourceGroup(ISeqConnection connection)
            : base("Pins", connection)
        {
        }

        public async Task<PinEntity> FindAsync(
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
            return await GroupGetAsync<PinEntity>("Item", parameters).ConfigureAwait(false);
        }

        public async Task<List<PinEntity>> ListAsync(
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

            return await GroupListAsync<PinEntity>("Items", parameters).ConfigureAwait(false);
        }

        public async Task<PinEntity> TemplateAsync()
        {
            return await GroupGetAsync<PinEntity>("Template").ConfigureAwait(false);
        }

        public async Task<PinEntity> AddAsync(PinEntity entity)
        {
            return await Client.PostAsync<PinEntity, PinEntity>(entity, "Create", entity).ConfigureAwait(false);
        }

        public async Task RemoveAsync(PinEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity).ConfigureAwait(false);
        }

        public async Task UpdateAsync(PinEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity).ConfigureAwait(false);
        }
    }
}