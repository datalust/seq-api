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
    /// Describes a node in a Seq cluster.
    /// </summary>
    public class ClusterNodeEntity : Entity
    {
        /// <summary>
        /// An informational name associated with the node.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The address the node will serve intra-cluster traffic on.
        /// </summary>
        public string ClusterListenUri { get; set; }
        
        /// <summary>
        /// The address the node will serve regular API requests on.
        /// </summary>
        public string InternalListenUri { get; set; }
        
        /// <summary>
        /// Whether any writes have occurred since the node's last completed sync.
        /// </summary>
        public bool IsUpToDate { get; set; }
        
        /// <summary>
        /// The time since the node's last completed sync operation.
        /// </summary>
        public double? DataAgeMilliseconds { get; set; }
        
        /// <summary>
        /// The time since the follower's active sync was started.
        /// </summary>
        public double? MillisecondsSinceActiveSync { get; set; }
        
        /// <summary>
        /// The time since the follower's last heartbeat.
        /// </summary>
        public double? MillisecondsSinceLastHeartbeat { get; set; }
        
        /// <summary>
        /// The total number of operations in the active sync.
        /// </summary>
        public int? TotalActiveOps { get; set;  }

        /// <summary>
        /// The remaining number of operations in the active sync.
        /// </summary>
        public int? RemainingActiveOps { get; set; }

        /// <summary>
        /// Whether the node is currently leading the cluster.
        /// </summary>
        public bool IsLeading { get; set; }
        
        /// <summary>
        /// Whether the node has connected to the cluster network.
        /// </summary>
        public bool IsConnected { get; set; }
    }
}
