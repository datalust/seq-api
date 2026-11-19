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
        internal SettingsResourceGroup(ILoadResourceGroup connection)
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

        /// <summary>
        /// Update a set of related settings together. Either all of the settings are updated, or none are. Changes to
        /// authentication provider settings are applied by reloading the provider, which ends all existing sessions
        /// when the provider is OpenID Connect-based. Requires the <c>System</c> permission.
        /// </summary>
        /// <param name="values">The settings to update. The <see cref="SettingsValuesPart.Username"/> and
        /// <see cref="SettingsValuesPart.Password"/> properties must be <c>null</c>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task ApplyAsync(SettingsValuesPart values, CancellationToken cancellationToken = default)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            await GroupPostAsync("Apply", values, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Check that proposed authentication settings can be used successfully, without applying them. Requires
        /// the <c>System</c> permission.
        /// </summary>
        /// <param name="test">The proposed settings.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The outcome of the test.</returns>
        public async Task<AuthenticationSettingsTestResultPart> TestAuthenticationSettingsAsync(SettingsValuesPart test, CancellationToken cancellationToken = default)
        {
            if (test == null) throw new ArgumentNullException(nameof(test));
            return await GroupPostAsync<SettingsValuesPart, AuthenticationSettingsTestResultPart>("AuthTest", test, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
