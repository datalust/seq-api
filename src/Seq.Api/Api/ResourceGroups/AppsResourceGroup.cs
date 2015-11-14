using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Apps;

namespace Seq.Api.ResourceGroups
{
    public class AppsResourceGroup : ApiResourceGroup
    {
        internal AppsResourceGroup(ISeqConnection connection)
            : base("Apps", connection)
        {
        }

        public async Task<AppEntity> FindAsync(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return await GroupGetAsync<AppEntity>("Item", new Dictionary<string, object> { { "id", id } });
        }

        public async Task<List<AppEntity>> ListAsync()
        {
            return await GroupListAsync<AppEntity>("Items");
        }

        public async Task<AppEntity> TemplateAsync()
        {
            return await GroupGetAsync<AppEntity>("Template");
        }

        public async Task<AppEntity> AddAsync(AppEntity entity)
        {
            return await Client.PostAsync<AppEntity, AppEntity>(entity, "Create", entity);
        }

        public async Task RemoveAsync(AppEntity entity)
        {
            await Client.DeleteAsync(entity, "Self", entity);
        }

        public async Task UpdateAsync(AppEntity entity)
        {
            await Client.PutAsync(entity, "Self", entity);
        }

        public async Task<AppEntity> InstallPackageAsync(string feedId, string packageId, string version = null)
        {
            if (feedId == null) throw new ArgumentNullException("feedId");
            if (packageId == null) throw new ArgumentNullException("packageId");
            var parameters = new Dictionary<string, object>{{ "feedId", feedId}, {"packageId", packageId}};
            if (version != null) parameters.Add("version", version);
            return await GroupPostAsync<object, AppEntity>("InstallPackage", new object(), parameters);
        }
    }
}