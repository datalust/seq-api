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
            if (id == null) throw new ArgumentNullException("id");
            var parameters = new Dictionary<string, object>
            {
                {"id", id},
                {"includeEvent", includeEvent},
                {"renderEvent", renderEvent},
                {"includeUser", includeUser}
            };
            return await GroupGetAsync<PinEntity>("Item", parameters);
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

            return await GroupListAsync<PinEntity>("Items", parameters);
        }

        public async Task<PinEntity> TemplateAsync()
        {
            return await GroupGetAsync<PinEntity>("Template");
        }

        public async Task<PinEntity> AddAsync(PinEntity entity)
        {
            return await Client.PostAsync<PinEntity, PinEntity>(entity, "Create", entity);
        }

        public async Task RemoveAsync(PinEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity);
        }

        public async Task UpdateAsync(PinEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity);
        }
    }
}