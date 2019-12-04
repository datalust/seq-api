// Copyright 2019 Datalust and contributors. 
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
using System.Threading.Tasks;
using System.Threading;
using Seq.Api.Model.Security;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on user roles.
    /// </summary>
    public class RolesResourceGroup : ApiResourceGroup
    {
        internal RolesResourceGroup(ISeqConnection connection)
            : base("Roles", connection)
        {
        }

        /// <summary>
        /// Find a role given its id.
        /// </summary>
        /// <param name="id">The id of a role to find.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The matching role.</returns>
        public async Task<RoleEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<RoleEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// List all roles.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing all roles available on the server.</returns>
        public async Task<List<RoleEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<RoleEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
