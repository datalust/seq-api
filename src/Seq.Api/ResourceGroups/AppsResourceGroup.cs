using System;
using System.Collections.Generic;
using System.Threading;
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

        public async Task<AppEntity> FindAsync(string id, CancellationToken token = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<AppEntity>("Item", new Dictionary<string, object> { { "id", id } }, token).ConfigureAwait(false);
        }

        public async Task<List<AppEntity>> ListAsync(CancellationToken token = default)
        {
            return await GroupListAsync<AppEntity>("Items", token: token).ConfigureAwait(false);
        }

        public async Task<AppEntity> TemplateAsync(CancellationToken token = default)
        {
            return await GroupGetAsync<AppEntity>("Template", token: token).ConfigureAwait(false);
        }

        public async Task<AppEntity> AddAsync(AppEntity entity, CancellationToken token = default)
        {
            return await Client.PostAsync<AppEntity, AppEntity>(entity, "Create", entity, token: token).ConfigureAwait(false);
        }

        public async Task RemoveAsync(AppEntity entity, CancellationToken token = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }

        public async Task UpdateAsync(AppEntity entity, CancellationToken token = default)
        {
            await Client.PutAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }

        public async Task<AppEntity> InstallPackageAsync(string feedId, string packageId, string version = null, CancellationToken token = default)
        {
            if (feedId == null) throw new ArgumentNullException(nameof(feedId));
            if (packageId == null) throw new ArgumentNullException(nameof(packageId));
            var parameters = new Dictionary<string, object>{{ "feedId", feedId}, {"packageId", packageId}};
            if (version != null) parameters.Add("version", version);
            return await GroupPostAsync<object, AppEntity>("InstallPackage", new object(), parameters, token).ConfigureAwait(false);
        }
    }
}