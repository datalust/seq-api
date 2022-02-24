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

using System.Collections.Generic;
using Seq.Api.Model.Signals;

#nullable enable

namespace Seq.Api.Model.Shared
{
    /// <summary>
    /// Specifies the context that queries and searches execute within.
    /// </summary>
    public class EvaluationContextPart
    {
        /// <summary>
        /// An unsaved or modified signal.
        /// </summary>
        public SignalEntity? Signal { get; set; }
        
        /// <summary>
        /// Values for free variables that appear in the query or search condition.
        /// </summary>
        /// <remarks>Variables will only be visible in the query or search being executed: any free variables
        /// in signal filters will remain undefined.</remarks>
        public Dictionary<string, object?>? Variables { get; set; }
    }
}
