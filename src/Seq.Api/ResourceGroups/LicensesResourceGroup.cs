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

        public async Task<LicenseEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<LicenseEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<LicenseEntity> FindCurrentAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<LicenseEntity>("Current", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<LicenseEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<LicenseEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(LicenseEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task DowngradeAsync(CancellationToken cancellationToken = default)
        {
            await GroupPostAsync("Downgrade", new object(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}