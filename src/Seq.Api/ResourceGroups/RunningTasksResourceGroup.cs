// Copyright Datalust and contributors. 
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
using Seq.Api.Model.Alerting;
using Seq.Api.Model.Tasks;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Inspect and cancel tasks running in the Seq server.
    /// </summary>
    public class RunningTasksResourceGroup : ApiResourceGroup
    {
        internal RunningTasksResourceGroup (ILoadResourceGroup connection)
            : base("RunningTasks", connection)
        {
        }

        /// <summary>
        /// Retrieve the task with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the task.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The task.</returns>
        public async Task<RunningTaskEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<RunningTaskEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve all tasks running on the server.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing all running tasks.</returns>
        public async Task<List<RunningTaskEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<RunningTaskEntity>("Items", null, cancellationToken).ConfigureAwait(false);
        }
    
        /// <summary>
        /// Request cancellation of a running task. The task must support cancellation; see <see cref="RunningTaskEntity.CanCancel"/>.
        /// </summary>
        /// <param name="entity">The task to cancel.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the cancellation operation itself to be canceled.</param>
        public async Task RequestCancellationAsync(AlertStateEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
