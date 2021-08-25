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
using Seq.Api.Model.Cluster;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on a Seq cluster.
    /// </summary>
    /// <remarks>Seq clustering initially supports a two-node replication
    /// topology.</remarks>
    public class ClusterNodesResourceGroup : ApiResourceGroup
    {
        internal ClusterNodesResourceGroup(ILoadResourceGroup connection)
            : base("ClusterNodes", connection)
        {
        }

        /// <summary>
        /// Retrieve the cluster node with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the cluster node.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The cluster node.</returns>
        public async Task<ClusterNodeEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<ClusterNodeEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve all known cluster nodes.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching dashboards.</returns>
        public async Task<List<ClusterNodeEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<ClusterNodeEntity>("Items", null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Manually demote a leader node. The operation will proceed asynchronously; the state of the node can be checked
        /// using <see cref="ListAsync"/> (the node will disappear when demotion has finished).
        /// </summary>
        /// <param name="leader">The leader node.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <remarks>If successfully failed over, the node must be restarted before it can join the cluster as follower.</remarks>
        public async Task DemoteAsync(ClusterNodeEntity leader, CancellationToken cancellationToken = default)
        {
            if (leader == null) throw new ArgumentNullException(nameof(leader));
            await Client.PostAsync(leader, "Demote", new object(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
