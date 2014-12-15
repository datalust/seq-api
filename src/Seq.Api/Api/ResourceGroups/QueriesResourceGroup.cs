using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Queries;

namespace Seq.Api.ResourceGroups
{
    public class QueriesResourceGroup : ApiResourceGroup
    {
        internal QueriesResourceGroup(ISeqConnection connection)
            : base("Queries", connection)
        {
        }

        public async Task<QueryEntity> FindAsync(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return await GroupGetAsync<QueryEntity>("Item", new Dictionary<string, object> { { "id", id } });
        }

        public async Task<List<QueryEntity>> ListAsync()
        {
            return await GroupListAsync<QueryEntity>("Items");
        }

        public async Task<QueryEntity> TemplateAsync()
        {
            return await GroupGetAsync<QueryEntity>("Template");
        }

        public async Task<QueryEntity> AddAsync(QueryEntity entity)
        {
            return await Client.PostAsync<QueryEntity, QueryEntity>(entity, "Self", entity);
        }

        public async Task RemoveAsync(QueryEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity);
        }

        public async Task UpdateAsync(QueryEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity);
        }
    }
}