using System;
using System.Collections.Generic;
using System.Threading;
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

        public async Task<SignalEntity> FindAsync(string id, CancellationToken token = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<SignalEntity>("Item", new Dictionary<string, object> { { "id", id } }, token).ConfigureAwait(false);
        }

        public async Task<List<SignalEntity>> ListAsync(string ownerId = null, bool shared = false, CancellationToken token)
        {
            var parameters = new Dictionary<string, object> { { "ownerId", ownerId }, { "shared", shared } };
            return await GroupListAsync<SignalEntity>("Items", parameters, token: token).ConfigureAwait(false);
        }

        public async Task<SignalEntity> TemplateAsync(CancellationToken token = default)
        {
            return await GroupGetAsync<SignalEntity>("Template", token: token).ConfigureAwait(false);
        }

        public async Task<SignalEntity> AddAsync(SignalEntity entity, CancellationToken token = default)
        {
            return await GroupCreateAsync<SignalEntity, SignalEntity>(entity, token: token).ConfigureAwait(false);
        }

        public async Task RemoveAsync(SignalEntity entity, CancellationToken token = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }

        public async Task UpdateAsync(SignalEntity entity, CancellationToken token = default)
        {
            await Client.PutAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }
    }
}