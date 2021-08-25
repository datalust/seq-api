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

namespace Seq.Api.Model.Cluster
{
    /// <summary>
    /// The role a node is acting in within a cluster of connected Seq instances.
    /// </summary>
    public enum NodeRole
    {
        /// <summary>
        /// The node is not part of a cluster.
        /// </summary>
        Standalone,
        
        /// <summary>
        /// The node is a replica, following the state of a leader node.
        /// </summary>
        Follower,
        
        /// <summary>
        /// The node is a replication leader.
        /// </summary>
        Leader
    }
}