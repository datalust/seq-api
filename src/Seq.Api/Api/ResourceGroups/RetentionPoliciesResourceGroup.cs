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
            return await GroupGetAsync<RetentionPolicyEntity>("Item", new Dictionary<string, object> { { "id", id } });
        }

        public async Task<List<RetentionPolicyEntity>> ListAsync()
        {
            return await GroupListAsync<RetentionPolicyEntity>("Items");
        }

        public async Task<RetentionPolicyEntity> TemplateAsync()
        {
            return await GroupGetAsync<RetentionPolicyEntity>("Template");
        }

        public async Task<RetentionPolicyEntity> AddAsync(RetentionPolicyEntity entity)
        {
            return await Client.PostAsync<RetentionPolicyEntity, RetentionPolicyEntity>(entity, "Create", entity);
        }

        public async Task RemoveAsync(RetentionPolicyEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity);
        }

        public async Task UpdateAsync(RetentionPolicyEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity);
        }
    }
}