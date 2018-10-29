using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.License;

namespace Seq.Api.ResourceGroups
{
    public class LicensesResourceGroup : ApiResourceGroup
    {
        internal LicensesResourceGroup(ISeqConnection connection)
            : base("Licenses", connection)
        {
        }

        public async Task<LicenseEntity> FindAsync(string id, CancellationToken token = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<LicenseEntity>("Item", new Dictionary<string, object> { { "id", id } }, token).ConfigureAwait(false);
        }

        public async Task<LicenseEntity> FindCurrentAsync(CancellationToken token = default)
        {
            return await GroupGetAsync<LicenseEntity>("Current", token: token).ConfigureAwait(false);
        }

        public async Task<List<LicenseEntity>> ListAsync(CancellationToken token = default)
        {
            return await GroupListAsync<LicenseEntity>("Items", token: token).ConfigureAwait(false);
        }

        public async Task UpdateAsync(LicenseEntity entity, CancellationToken token = default)
        {
            await Client.PutAsync(entity, "Self", entity, token: token).ConfigureAwait(false);
        }

        public async Task DowngradeAsync(CancellationToken token = default)
        {
            await GroupPostAsync("Downgrade", new object(), token: token).ConfigureAwait(false);
        }
    }
}