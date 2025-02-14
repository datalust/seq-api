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
    /// A status value returned from a health check endpoint.
    /// </summary>
    /// <remarks>Note that HTTP status code values returned from health checks should be inspected prior to
    /// reading status information from the health check response payload.</remarks>
    public enum HealthStatus
    {
        /// <summary>
        /// The target is healthy.
        /// </summary>
        Healthy,
        
        /// <summary>
        /// The target is functioning in a degraded state; attention is required.
        /// </summary>
        Degraded,
        
        /// <summary>
        /// The target is unhealthy.
        /// </summary>
        Unhealthy
    }
}
