// Copyright 2014-2019 Datalust and contributors. 
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

namespace Seq.Api.Model.Signals
{
    /// <summary>
    /// The type of expression represented by a <see cref="SignalExpressionPart"/>.
    /// </summary>
    public enum SignalExpressionKind
    {
        /// <summary>
        /// Uninitialized value.
        /// </summary>
        None,

        /// <summary>
        /// The expression identifies a single signal.
        /// </summary>
        Signal,

        /// <summary>
        /// The expression is an intersection (<c>and</c> operation) of two signal expressions.
        /// </summary>
        Intersection,

        /// <summary>
        /// The expression is a union (<c>or</c> operation) of two signal expressions.
        /// </summary>
        Union
    }
}
