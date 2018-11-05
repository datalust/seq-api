using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Settings;

namespace Seq.Api.ResourceGroups
{
    public class SettingsResourceGroup : ApiResourceGroup
    {
        internal SettingsResourceGroup(ISeqConnection connection)
            : base("Settings", connection)
        {
        }

        public async Task<SettingEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<SettingEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<SettingEntity> FindNamedAsync(SettingName name, CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<SettingEntity>(name.ToString(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<SettingEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<SettingEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<SettingEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<SettingEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<SettingEntity> AddAsync(SettingEntity entity, CancellationToken cancellationToken = default)
        {
            return await Client.PostAsync<SettingEntity, SettingEntity>(entity, "Create", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task RemoveAsync(SettingEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(SettingEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<InternalErrorReportingSettingsPart> GetInternalErrorReportingAsync()
        {
            return await GroupGetAsync<InternalErrorReportingSettingsPart>("InternalErrorReporting").ConfigureAwait(false);
        }

        public async Task UpdateInternalErrorReportingAsync(InternalErrorReportingSettingsPart internalErrorReporting)
        {
            await GroupPutAsync("InternalErrorReporting", internalErrorReporting).ConfigureAwait(false);
        }
    }
}
