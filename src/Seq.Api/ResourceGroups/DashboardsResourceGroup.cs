using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Monitoring;

namespace Seq.Api.ResourceGroups
{
    public class DashboardsResourceGroup : ApiResourceGroup
    {
        internal DashboardsResourceGroup(ISeqConnection connection)
            : base("Dashboards", connection)
        {
        }

        public async Task<DashboardEntity> FindAsync(string id, CancellationToken token = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<DashboardEntity>("Item", new Dictionary<string, object> { { "id", id } }, token).ConfigureAwait(false);
        }

        public async Task<List<DashboardEntity>> ListAsync(string ownerId = null, bool shared = false, CancellationToken token = default)
        {
            var parameters = new Dictionary<string, object> { { "ownerId", ownerId }, { "shared", shared } };
            return await GroupListAsync<DashboardEntity>("Items", parameters, token).ConfigureAwait(false);
        }

        public async Task<DashboardEntity> TemplateAsync(CancellationToken token = default)
        {
            return await GroupGetAsync<DashboardEntity>("Template", token: token).ConfigureAwait(false);
        }

        public async Task<DashboardEntity> AddAsync(DashboardEntity entity, CancellationToken token = default)
        {
            return await GroupCreateAsync<DashboardEntity, DashboardEntity>(entity, token: token).ConfigureAwait(false);
        }

        public async Task RemoveAsync(DashboardEntity entity, CancellationToken token = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }

        public async Task UpdateAsync(DashboardEntity entity, CancellationToken token = default)
        {
            await Client.PutAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }
    }
}