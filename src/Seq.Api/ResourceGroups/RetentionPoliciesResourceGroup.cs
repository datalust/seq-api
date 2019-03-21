using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Retention;

namespace Seq.Api.ResourceGroups
{
    public class RetentionPoliciesResourceGroup : ApiResourceGroup
    {
        internal RetentionPoliciesResourceGroup(ISeqConnection connection)
            : base("RetentionPolicies", connection)
        {
        }

        public async Task<RetentionPolicyEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<RetentionPolicyEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<RetentionPolicyEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<RetentionPolicyEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<RetentionPolicyEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<RetentionPolicyEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<RetentionPolicyEntity> AddAsync(RetentionPolicyEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<RetentionPolicyEntity, RetentionPolicyEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task RemoveAsync(RetentionPolicyEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(RetentionPolicyEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}