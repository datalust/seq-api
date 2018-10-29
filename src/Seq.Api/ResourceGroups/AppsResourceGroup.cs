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

        public async Task<AppEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<AppEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<AppEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<AppEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<AppEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<AppEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<AppEntity> AddAsync(AppEntity entity, CancellationToken cancellationToken = default)
        {
            return await Client.PostAsync<AppEntity, AppEntity>(entity, "Create", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task RemoveAsync(AppEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(AppEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<AppEntity> InstallPackageAsync(string feedId, string packageId, string version = null, CancellationToken cancellationToken = default)
        {
            if (feedId == null) throw new ArgumentNullException(nameof(feedId));
            if (packageId == null) throw new ArgumentNullException(nameof(packageId));
            var parameters = new Dictionary<string, object> { { "feedId", feedId }, { "packageId", packageId } };
            if (version != null) parameters.Add("version", version);
            return await GroupPostAsync<object, AppEntity>("InstallPackage", new object(), parameters, cancellationToken).ConfigureAwait(false);
        }

        public async Task<AppEntity> UpdatePackageAsync(AppEntity entity, string version = null, bool force = false)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var parameters = new Dictionary<string, object>();
            if (force) parameters.Add("force", true);
            if (version != null) parameters.Add("version", version);
            return await Client.PostAsync<object, AppEntity>(entity, "UpdatePackage", new object(), parameters).ConfigureAwait(false);
        }
    }
}