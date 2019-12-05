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

namespace Seq.Api.Model.Expressions
{
    /// <summary>
    /// Information about a filter expression.
    /// </summary>
    public class ExpressionPart
    {
        /// <summary>
        /// The expression in strict syntax.
        /// </summary>
        public string StrictExpression { get; set; }

        /// <summary>
        /// Whether the expression was considered to be a free text search.
        /// </summary>
        public bool MatchedAsText { get; set; }

        /// <summary>
        /// If <see cref="MatchedAsText"/> is <c>true</c>, the reason that the
        /// expression was not considered a valid structured expression.
        /// </summary>
        public string ReasonIfMatchedAsText { get; set; }
    }
}
