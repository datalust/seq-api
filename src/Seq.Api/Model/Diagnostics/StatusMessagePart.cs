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

namespace Seq.Api.Model.Diagnostics
{
    /// <summary>
    /// A status message relating to the Seq server.
    /// </summary>
    public class StatusMessagePart
    {
        /// <summary>
        /// A descriptive level (<c>Information</c>, <c>Warning</c>, or <c>Error</c>).
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// The message text.
        /// </summary>
        public string Message { get; set; }
    }
}
