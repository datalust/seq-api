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
        /// The role the node is currently acting in.
        /// </summary>
        public NodeRole Role { get; set; }
        
        /// <summary>
        /// An informational name associated with the node.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// An informational representation of the storage generation committed to the node.
        /// </summary>
        public string Generation { get; set; }
        
        /// <summary>
        /// Whether any writes have occurred since the node's last completed sync.
        /// </summary>
        public bool? IsUpToDate { get; set; }
        
        /// <summary>
        /// The time since the node's last completed sync operation.
        /// </summary>
        public double? MillisecondsSinceLastSync { get; set; }

        /// <summary>
        /// An informational description of the node's current state, or <c langword="null">null</c> if no additional
        /// information about the node is available.
        /// </summary>
        public string StateDescription { get; set; }
    }
}
