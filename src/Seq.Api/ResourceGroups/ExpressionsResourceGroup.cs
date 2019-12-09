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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Expressions;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on queries and filter expressions.
    /// </summary>
    public class ExpressionsResourceGroup : ApiResourceGroup
    {
        internal ExpressionsResourceGroup(ISeqConnection connection)
            : base("Expressions", connection)
        {
        }

        /// <summary>
        /// Convert an expression in the relaxed syntax supported by the filter bar, to the strictly-valid
        /// syntax required by API interactions.
        /// </summary>
        /// <param name="fuzzy">The (potentially) relaxed-syntax expression.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The expression in a strictly-valid form.</returns>
        public Task<ExpressionPart> ToStrictAsync(string fuzzy, CancellationToken cancellationToken = default)
        {
            return GroupGetAsync<ExpressionPart>("ToStrict", new Dictionary<string, object>
            {
                {"fuzzy", fuzzy}
            }, cancellationToken);
        }

        /// <summary>
        /// Convert an expression in the relaxed syntax supported by the filter bar, to the strict and limited
        /// syntax required within SQL queries.
        /// </summary>
        /// <param name="fuzzy">The (potentially) relaxed-syntax expression.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The expression in a form that can be used within SQL queries.</returns>
        public Task<ExpressionPart> ToSqlAsync(string fuzzy, CancellationToken cancellationToken = default)
        {
            return GroupGetAsync<ExpressionPart>("ToSql", new Dictionary<string, object>
            {
                {"fuzzy", fuzzy}
            }, cancellationToken);
        }
    }
}