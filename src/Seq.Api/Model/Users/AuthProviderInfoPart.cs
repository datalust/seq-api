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

namespace Seq.Api.Model.Users
{
    /// <summary>
    /// An authentication provider supported by the server.
    /// </summary>
    public class AuthProviderInfoPart
    {
        /// <summary>
        /// The friendly, human-readable name of the authentication provider.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The URL where the user can log in with the provider.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// If <c>true</c>, the provider is shown as an additional/alternative way to
        /// log in using the default provider.
        /// </summary>
        public bool IsAlternative { get; set; }

        /// <summary>
        /// A template for the URL where the user can log in.
        /// </summary>
        public Link Challenge { get; set; }
    }
}
