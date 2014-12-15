using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Views;

namespace Seq.Api.ResourceGroups
{
    public class ViewsResourceGroup : ApiResourceGroup
    {
        internal ViewsResourceGroup(ISeqConnection connection)
            : base("Views", connection)
        {
        }

        public async Task<ViewEntity> FindAsync(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return await GroupGetAsync<ViewEntity>("Item", new Dictionary<string, object> { { "id", id } });
        }

        public async Task<List<ViewEntity>> ListAsync()
        {
            return await GroupListAsync<ViewEntity>("Items");
        }

        public async Task<ViewEntity> TemplateAsync()
        {
            return await GroupGetAsync<ViewEntity>("Template");
        }

        public async Task<ViewEntity> AddAsync(ViewEntity entity)
        {
            return await Client.PostAsync<ViewEntity, ViewEntity>(entity, "Self", entity);
        }

        public async Task RemoveAsync(ViewEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity);
        }

        public async Task UpdateAsync(ViewEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity);
        }
    }
}
