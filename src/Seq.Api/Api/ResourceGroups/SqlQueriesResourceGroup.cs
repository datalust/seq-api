using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.SqlQueries;

namespace Seq.Api.ResourceGroups
{
    public class SqlQueriesResourceGroup : ApiResourceGroup
    {
        internal SqlQueriesResourceGroup(ISeqConnection connection)
            : base("SqlQueries", connection)
        {
        }

        public async Task<SqlQueryEntity> FindAsync(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<SqlQueryEntity>("Item", new Dictionary<string, object> { { "id", id } }).ConfigureAwait(false);
        }

        public async Task<List<SqlQueryEntity>> ListAsync(string ownerId = null, bool shared = false)
        {
            var parameters = new Dictionary<string, object>{{"ownerId", ownerId}, {"shared", shared}};
            return await GroupListAsync<SqlQueryEntity>("Items", parameters).ConfigureAwait(false);
        }

        public async Task<SqlQueryEntity> TemplateAsync()
        {
            return await GroupGetAsync<SqlQueryEntity>("Template").ConfigureAwait(false);
        }

        public async Task<SqlQueryEntity> AddAsync(SqlQueryEntity entity)
        {
            return await Client.PostAsync<SqlQueryEntity, SqlQueryEntity>(entity, "Create", entity).ConfigureAwait(false);
        }

        public async Task RemoveAsync(SqlQueryEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity).ConfigureAwait(false);
        }

        public async Task UpdateAsync(SqlQueryEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity).ConfigureAwait(false);
        }
    }
}
