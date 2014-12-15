using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Feeds;

namespace Seq.Api.ResourceGroups
{
    public class FeedsResourceGroup : ApiResourceGroup
    {
        internal FeedsResourceGroup(ISeqConnection connection)
            : base("Feeds", connection)
        {
        }

        public async Task<NuGetFeedEntity> FindAsync(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return await GroupGetAsync<NuGetFeedEntity>("Item", new Dictionary<string, object> { { "id", id } });
        }

        public async Task<List<NuGetFeedEntity>> ListAsync()
        {
            return await GroupListAsync<NuGetFeedEntity>("Items");
        }

        public async Task<NuGetFeedEntity> TemplateAsync()
        {
            return await GroupGetAsync<NuGetFeedEntity>("Template");
        }

        public async Task<NuGetFeedEntity> AddAsync(NuGetFeedEntity entity)
        {
            return await Client.PostAsync<NuGetFeedEntity, NuGetFeedEntity>(entity, "Self", entity);
        }

        public async Task RemoveAsync(NuGetFeedEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity);
        }

        public async Task UpdateAsync(NuGetFeedEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity);
        }
    }
}