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

namespace Seq.Api.Model
{
    /// <summary>
    /// A uniquely-identifiable resource available from the Seq HTTP API.
    /// </summary>
    /// <remarks>Entities are the persistent top-level resources that have a stable
    /// URI. The API client uses the contrasting suffix <c>*Part</c> to designate
    /// resources that are transient or not directly addressable.</remarks>
    public abstract class Entity : ILinked
    {
        /// <summary>
        /// Construct an <see cref="Entity"/>.
        /// </summary>
        protected Entity()
        {
            Links = new LinkCollection();
        }

        /// <summary>
        /// The entity's unique identifier. This will be <c>null</c> if a newly-instantiated <see cref="Entity"/>
        /// has not yet been sent to the server.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A collection of outbound links from the entity. This will be empty if the entity
        /// was instantiated locally and not received from the API.
        /// </summary>
        public LinkCollection Links { get; set; }
    }
}
