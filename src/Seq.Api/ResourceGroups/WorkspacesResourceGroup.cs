using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Workspaces;

namespace Seq.Api.ResourceGroups
{
    public class WorkspacesResourceGroup : ApiResourceGroup
    {
        internal WorkspacesResourceGroup(ISeqConnection connection)
            : base("Workspaces", connection)
        {
        }

        public async Task<WorkspaceEntity> FindAsync(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<WorkspaceEntity>("Item", new Dictionary<string, object> { { "id", id } }).ConfigureAwait(false);
        }

        public async Task<List<WorkspaceEntity>> ListAsync(string ownerId = null, bool shared = false)
        {
            var parameters = new Dictionary<string, object> { { "ownerId", ownerId }, { "shared", shared } };
            return await GroupListAsync<WorkspaceEntity>("Items", parameters).ConfigureAwait(false);
        }

        public async Task<WorkspaceEntity> TemplateAsync()
        {
            return await GroupGetAsync<WorkspaceEntity>("Template").ConfigureAwait(false);
        }

        public async Task<WorkspaceEntity> AddAsync(WorkspaceEntity entity)
        {
            return await GroupCreateAsync<WorkspaceEntity, WorkspaceEntity>(entity).ConfigureAwait(false);
        }

        public async Task RemoveAsync(WorkspaceEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity).ConfigureAwait(false);
        }

        public async Task UpdateAsync(WorkspaceEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity).ConfigureAwait(false);
        }
    }
}
