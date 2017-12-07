using System;
using System.Collections.Generic;
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

        public async Task<DashboardEntity> FindAsync(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<DashboardEntity>("Item", new Dictionary<string, object> { { "id", id } }).ConfigureAwait(false);
        }

        public async Task<List<DashboardEntity>> ListAsync(string ownerId = null, bool shared = false)
        {
            var parameters = new Dictionary<string, object> { { "ownerId", ownerId }, { "shared", shared } };
            return await GroupListAsync<DashboardEntity>("Items", parameters).ConfigureAwait(false);
        }

        public async Task<DashboardEntity> TemplateAsync()
        {
            return await GroupGetAsync<DashboardEntity>("Template").ConfigureAwait(false);
        }

        public async Task<DashboardEntity> AddAsync(DashboardEntity entity)
        {
            return await Client.PostAsync<DashboardEntity, DashboardEntity>(entity, "Create", entity).ConfigureAwait(false);
        }

        public async Task RemoveAsync(DashboardEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity).ConfigureAwait(false);
        }

        public async Task UpdateAsync(DashboardEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity).ConfigureAwait(false);
        }
    }
}