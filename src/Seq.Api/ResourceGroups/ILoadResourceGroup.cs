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

using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Client;
using Seq.Api.Model;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// The contract between resource groups and the implementing connection type.
    /// </summary>
    /// <remarks>This interface is an implementation detail that should not be relied on by
    /// application-level consumers.</remarks>
    public interface ILoadResourceGroup
    {
        /// <summary>
        /// Load the resource group with the given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The resource group name. The name is the simple form, for example,
        /// <c>"Dashboards"</c>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The requested resource group.</returns>
        Task<ResourceGroup> LoadResourceGroupAsync(string name, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// The underlying Seq API client.
        /// </summary>
        SeqApiClient Client { get; }
    }
}
