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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Updates;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on known available Seq versions.
    /// </summary>
    public class UpdatesResourceGroup : ApiResourceGroup
    {
        internal UpdatesResourceGroup(ILoadResourceGroup connection)
            : base("Updates", connection)
        {
        }

        /// <summary>
        /// Retrieve available updates.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list of available updates.</returns>
        public async Task<List<AvailableUpdateEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<AvailableUpdateEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
