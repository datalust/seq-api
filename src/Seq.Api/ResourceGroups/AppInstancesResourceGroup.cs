using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.AppInstances;

namespace Seq.Api.ResourceGroups
{
    public class AppInstancesResourceGroup : ApiResourceGroup
    {
        internal AppInstancesResourceGroup(ISeqConnection connection)
            : base("AppInstances", connection)
        {
        }

        public async Task<AppInstanceEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<AppInstanceEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<AppInstanceEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<AppInstanceEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<AppInstanceEntity> TemplateAsync(string appId, CancellationToken cancellationToken = default)
        {
            if (appId == null) throw new ArgumentNullException(nameof(appId));
            return await GroupGetAsync<AppInstanceEntity>("Template", new Dictionary<string, object> { { "appId", appId } }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<AppInstanceEntity> AddAsync(AppInstanceEntity entity, bool runOnExisting = false, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return await Client.PostAsync<AppInstanceEntity, AppInstanceEntity>(entity, "Create", entity, new Dictionary<string, object> { { "runOnExisting", runOnExisting } }, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task RemoveAsync(AppInstanceEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(AppInstanceEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task InvokeAsync(AppInstanceEntity entity, string eventId, IReadOnlyDictionary<string, string> settingOverrides, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (eventId == null) throw new ArgumentNullException(nameof(eventId));

            var postedSettings = settingOverrides ?? new Dictionary<string, string>();
            await Client.PostAsync(entity, "Invoke", postedSettings, new Dictionary<string, object>{{"eventId", eventId}}, cancellationToken);
        }
    }
}
