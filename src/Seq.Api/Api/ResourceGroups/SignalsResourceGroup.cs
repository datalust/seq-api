using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Signals;

namespace Seq.Api.ResourceGroups
{
    public class SignalsResourceGroup : ApiResourceGroup
    {
        internal SignalsResourceGroup(ISeqConnection connection)
            : base("Signals", connection)
        {
        }

        public async Task<SignalEntity> FindAsync(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return await GroupGetAsync<SignalEntity>("Item", new Dictionary<string, object> { { "id", id } });
        }

        public async Task<List<SignalEntity>> ListAsync()
        {
            return await GroupListAsync<SignalEntity>("Items");
        }

        public async Task<SignalEntity> TemplateAsync()
        {
            return await GroupGetAsync<SignalEntity>("Template");
        }

        public async Task<SignalEntity> AddAsync(SignalEntity entity)
        {
            return await Client.PostAsync<SignalEntity, SignalEntity>(entity, "Create", entity);
        }

        public async Task RemoveAsync(SignalEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity);
        }

        public async Task UpdateAsync(SignalEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity);
        }
    }
}