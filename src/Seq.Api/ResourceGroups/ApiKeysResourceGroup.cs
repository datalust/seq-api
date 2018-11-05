using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Inputs;

namespace Seq.Api.ResourceGroups
{
    public class ApiKeysResourceGroup : ApiResourceGroup
    {
        internal ApiKeysResourceGroup(ISeqConnection connection)
            : base("ApiKeys", connection)
        {
        }

        public async Task<ApiKeyEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<ApiKeyEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<ApiKeyEntity>> ListAsync(string ownerId = null, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object> { { "ownerId", ownerId } };
            return await GroupListAsync<ApiKeyEntity>("Items", parameters, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiKeyEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<ApiKeyEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiKeyEntity> AddAsync(ApiKeyEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<ApiKeyEntity, ApiKeyEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task RemoveAsync(ApiKeyEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(ApiKeyEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
