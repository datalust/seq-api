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
using Seq.Api.Model.Settings;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on system settings.
    /// </summary>
    public class SettingsResourceGroup : ApiResourceGroup
    {
        internal SettingsResourceGroup(ISeqConnection connection)
            : base("Settings", connection)
        {
        }

        /// <summary>
        /// Retrieve the setting with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the setting.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The setting.</returns>
        public async Task<SettingEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<SettingEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the setting with the given name; throws if the entity does not exist.
        /// </summary>
        /// <param name="name">The name of the setting to retrieve. See also <see cref="SettingName"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The setting.</returns>
        public async Task<SettingEntity> FindNamedAsync(SettingName name, CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<SettingEntity>(name.ToString(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an existing setting.
        /// </summary>
        /// <param name="entity">The setting to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task UpdateAsync(SettingEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get internal error reporting settings.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>Internal error reporting settings.</returns>
        public async Task<InternalErrorReportingSettingsPart> GetInternalErrorReportingAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<InternalErrorReportingSettingsPart>("InternalErrorReporting", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update internal error reporting settings.
        /// </summary>
        /// <param name="internalErrorReporting">New internal error reporting settings.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task UpdateInternalErrorReportingAsync(InternalErrorReportingSettingsPart internalErrorReporting, CancellationToken cancellationToken = default)
        {
            await GroupPutAsync("InternalErrorReporting", internalErrorReporting, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
