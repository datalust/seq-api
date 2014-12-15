using System;
using System.Collections.Generic;
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

        public async Task<ApiKeyEntity> FindAsync(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return await GroupGetAsync<ApiKeyEntity>("Item", new Dictionary<string, object> {{"id", id}});
        }

        public async Task<List<ApiKeyEntity>> ListAsync()
        {
            return await GroupListAsync<ApiKeyEntity>("Items");
        }

        public async Task<ApiKeyEntity> TemplateAsync()
        {
            return await GroupGetAsync<ApiKeyEntity>("Template");
        }

        public async Task<ApiKeyEntity> AddAsync(ApiKeyEntity entity)
        {
            return await Client.PostAsync<ApiKeyEntity, ApiKeyEntity>(entity, "Self", entity);
        }

        public async Task RemoveAsync(ApiKeyEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity);
        }

        public async Task UpdateAsync(ApiKeyEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity);
        }
    }
}
