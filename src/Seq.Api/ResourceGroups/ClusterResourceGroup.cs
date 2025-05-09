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
using Newtonsoft.Json;
using Seq.Api.Model.Cluster;
using Seq.Api.Model.Shared;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on the Seq cluster.
    /// </summary>
    public class ClusterResourceGroup : ApiResourceGroup
    {
        internal ClusterResourceGroup(ILoadResourceGroup connection)
            : base("Cluster", connection)
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
        /// Manually cycle leadership in the cluster by draining active requests and taking the current leader node
        /// offline. Warning, check cluster health first as this may leave the cluster unable to service requests if
        /// invoked on an unhealthy cluster.
        /// </summary>
        /// <param name="entity">The entity to drain.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        public async Task DrainAsync(ClusterNodeEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.PostAsync(entity, "Drain", new object(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Check the health of the cluster.
        /// </summary>
        /// <remarks>This method will suppress exceptions generated when connecting to the target endpoint, and will return
        /// a placeholder (unhealthy) response in those cases.</remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The information reported by the cluster health endpoint.</returns>
        public async Task<HealthCheckResultPart> CheckHealthAsync(CancellationToken cancellationToken = default)
        {
            var group = await LoadGroupAsync(cancellationToken).ConfigureAwait(false);
            var link = group.Links["Health"].GetUri();
            var response = await Client.HttpClient.GetAsync(link, cancellationToken: cancellationToken).ConfigureAwait(false);
            
            try
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<HealthCheckResultPart>(content);
            }
            catch
            {
                ErrorPart error = null;
                try
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    error = JsonConvert.DeserializeObject<ErrorPart>(content);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch { }

                return new HealthCheckResultPart
                {
                    Status = HealthStatus.Unhealthy,
                    Description = error?.Error ??
                                  $"Could not connect to the Seq API endpoint ({(int)response.StatusCode}/{response.StatusCode})."
                };
            }
        }
    }
}
