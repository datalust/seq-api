using System;
using System.Collections.Generic;
using System.Threading;
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

        public async Task<SqlQueryEntity> FindAsync(string id, CancellationToken token = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<SqlQueryEntity>("Item", new Dictionary<string, object> { { "id", id } }, token).ConfigureAwait(false);
        }

        public async Task<List<SqlQueryEntity>> ListAsync(CancellationToken token = default)
        {
            return await GroupListAsync<SqlQueryEntity>("Items", token: token).ConfigureAwait(false);
        }

        public async Task<SqlQueryEntity> TemplateAsync(CancellationToken token = default)
        {
            return await GroupGetAsync<SqlQueryEntity>("Template", token: token).ConfigureAwait(false);
        }

        public async Task<SqlQueryEntity> AddAsync(SqlQueryEntity entity, CancellationToken token = default)
        {
            return await Client.PostAsync<SqlQueryEntity, SqlQueryEntity>(entity, "Create", entity, token: token).ConfigureAwait(false);
        }

        public async Task RemoveAsync(SqlQueryEntity entity, CancellationToken token = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }

        public async Task UpdateAsync(SqlQueryEntity entity, CancellationToken token = default)
        {
            await Client.PutAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }
    }
}
