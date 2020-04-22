// Copyright © Datalust and contributors. 
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model;
using Seq.Api.Model.AppInstances;
using Seq.Api.Model.Apps;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on app instances. App instances are individual processes based on a running <see cref="AppEntity"/> that can
    /// read from and write to the Seq event stream.
    /// </summary>
    public class AppInstancesResourceGroup : ApiResourceGroup
    {
        internal AppInstancesResourceGroup(ISeqConnection connection)
            : base("AppInstances", connection)
        {
        }

        /// <summary>
        /// Retrieve the app instance with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the app instance.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The app instance.</returns>
        public async Task<AppInstanceEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<AppInstanceEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve app instances.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching app instances.</returns>
        public async Task<List<AppInstanceEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<AppInstanceEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Construct an app instance with appropriate app settings and server defaults pre-initialized.
        /// </summary>
        /// <param name="appId">The id of the <see cref="AppEntity"/> to create an instance of.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The unsaved app instance.</returns>
        public async Task<AppInstanceEntity> TemplateAsync(string appId, CancellationToken cancellationToken = default)
        {
            if (appId == null) throw new ArgumentNullException(nameof(appId));
            return await GroupGetAsync<AppInstanceEntity>("Template", new Dictionary<string, object> { { "appId", appId } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a new app instance.
        /// </summary>
        /// <param name="entity">The app instance to add.</param>
        /// <param name="runOnExisting">If <c>true</c>, events already on the server will be sent to the app. Note that this requires disk buffering and persistent bookmarks
        /// for the app, which is not recommended for performance reasons.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The app instance, with server-allocated properties such as <see cref="Entity.Id"/> initialized.</returns>
        public async Task<AppInstanceEntity> AddAsync(AppInstanceEntity entity, bool runOnExisting = false, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return await GroupCreateAsync<AppInstanceEntity, AppInstanceEntity>(entity, new Dictionary<string, object> { { "runOnExisting", runOnExisting } }, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Remove an existing app instance.
        /// </summary>
        /// <param name="entity">The app instance to remove.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task RemoveAsync(AppInstanceEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an existing app instance.
        /// </summary>
        /// <param name="entity">The app instance to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task UpdateAsync(AppInstanceEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Send the event with id <paramref name="eventId"/> to the app instance <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">The app instance to invoke.</param>
        /// <param name="eventId">The id of an event to send to the app.</param>
        /// <param name="settingOverrides">Values for any overridable settings exposed by the app instance.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task InvokeAsync(AppInstanceEntity entity, string eventId, IReadOnlyDictionary<string, string> settingOverrides, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (eventId == null) throw new ArgumentNullException(nameof(eventId));

            var postedSettings = settingOverrides ?? new Dictionary<string, string>();
            await Client.PostAsync(entity, "Invoke", postedSettings, new Dictionary<string, object>{{"eventId", eventId}}, cancellationToken);
        }
    }
}
