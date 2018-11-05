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

        public async Task<SignalEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<SignalEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<SignalEntity>> ListAsync(string ownerId = null, bool shared = false, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object> { { "ownerId", ownerId }, { "shared", shared } };
            return await GroupListAsync<SignalEntity>("Items", parameters, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<SignalEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<SignalEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<SignalEntity> AddAsync(SignalEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<SignalEntity, SignalEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task RemoveAsync(SignalEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(SignalEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}