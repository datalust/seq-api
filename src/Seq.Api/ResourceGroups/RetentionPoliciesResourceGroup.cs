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

        public async Task<RetentionPolicyEntity> FindAsync(string id, CancellationToken token = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<RetentionPolicyEntity>("Item", new Dictionary<string, object> { { "id", id } }, token).ConfigureAwait(false);
        }

        public async Task<List<RetentionPolicyEntity>> ListAsync(CancellationToken token = default)
        {
            return await GroupListAsync<RetentionPolicyEntity>("Items", token: token).ConfigureAwait(false);
        }

        public async Task<RetentionPolicyEntity> TemplateAsync(CancellationToken token = default)
        {
            return await GroupGetAsync<RetentionPolicyEntity>("Template", token: token).ConfigureAwait(false);
        }

        public async Task<RetentionPolicyEntity> AddAsync(RetentionPolicyEntity entity, CancellationToken token = default)
        {
            return await Client.PostAsync<RetentionPolicyEntity, RetentionPolicyEntity>(entity, "Create", entity, token: token).ConfigureAwait(false);
        }

        public async Task RemoveAsync(RetentionPolicyEntity entity, CancellationToken token = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }

        public async Task UpdateAsync(RetentionPolicyEntity entity, CancellationToken token = default)
        {
            await Client.PutAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }
    }
}