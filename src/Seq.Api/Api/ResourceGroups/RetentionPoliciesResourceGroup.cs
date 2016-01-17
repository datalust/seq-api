using System;
using System.Collections.Generic;
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

        public async Task<RetentionPolicyEntity> FindAsync(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return await GroupGetAsync<RetentionPolicyEntity>("Item", new Dictionary<string, object> { { "id", id } }).ConfigureAwait(false);
        }

        public async Task<List<RetentionPolicyEntity>> ListAsync()
        {
            return await GroupListAsync<RetentionPolicyEntity>("Items").ConfigureAwait(false);
        }

        public async Task<RetentionPolicyEntity> TemplateAsync()
        {
            return await GroupGetAsync<RetentionPolicyEntity>("Template").ConfigureAwait(false);
        }

        public async Task<RetentionPolicyEntity> AddAsync(RetentionPolicyEntity entity)
        {
            return await Client.PostAsync<RetentionPolicyEntity, RetentionPolicyEntity>(entity, "Create", entity).ConfigureAwait(false);
        }

        public async Task RemoveAsync(RetentionPolicyEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity).ConfigureAwait(false);
        }

        public async Task UpdateAsync(RetentionPolicyEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity).ConfigureAwait(false);
        }
    }
}