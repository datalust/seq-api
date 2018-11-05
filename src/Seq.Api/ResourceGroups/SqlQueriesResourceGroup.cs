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

        public async Task<SqlQueryEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<SqlQueryEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<SqlQueryEntity>> ListAsync(string ownerId = null, bool shared = false, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object> { { "ownerId", ownerId }, { "shared", shared } };
            return await GroupListAsync<SqlQueryEntity>("Items", parameters, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<SqlQueryEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<SqlQueryEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<SqlQueryEntity> AddAsync(SqlQueryEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<SqlQueryEntity, SqlQueryEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task RemoveAsync(SqlQueryEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(SqlQueryEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
