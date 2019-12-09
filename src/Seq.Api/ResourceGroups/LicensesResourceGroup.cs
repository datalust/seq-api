// Copyright 2014-2019 Datalust and contributors. 
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
using Seq.Api.Model.License;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on the Seq license certificate.
    /// </summary>
    public class LicensesResourceGroup : ApiResourceGroup
    {
        internal LicensesResourceGroup(ISeqConnection connection)
            : base("Licenses", connection)
        {
        }

        /// <summary>
        /// Retrieve the license with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the license.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The license.</returns>
        public async Task<LicenseEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<LicenseEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the license being used by the server.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The license.</returns>
        public async Task<LicenseEntity> FindCurrentAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<LicenseEntity>("Current", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve licenses.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching licenses.</returns>
        public async Task<List<LicenseEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<LicenseEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an existing license.
        /// </summary>
        /// <param name="entity">The license to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task UpdateAsync(LicenseEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PutAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove the current license, causing the server to fall back to the default configuration.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task DowngradeAsync(CancellationToken cancellationToken = default)
        {
            await GroupPostAsync("Downgrade", new object(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}