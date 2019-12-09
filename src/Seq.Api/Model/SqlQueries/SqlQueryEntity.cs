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

using Seq.Api.Model.Security;

namespace Seq.Api.Model.SqlQueries
{
    /// <summary>
    /// A saved SQL-style query.
    /// </summary>
    public class SqlQueryEntity : Entity
    {
        /// <summary>
        /// Construct a <see cref="SqlQueryEntity"/>.
        /// </summary>
        public SqlQueryEntity()
        {
            Title = "New SQL Query";
            Sql = "";
        }

        /// <summary>
        /// A friendly, human-readable name for the query.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A long-form description of the query.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The query text.
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// If <c>true</c>, only users with <see cref="Permission.Setup"/> permission can edit the signal.
        /// </summary>
        public bool IsProtected { get; set; }

        /// <summary>
        /// The id of a user who owns this query. If <c>null</c>, the query is shared.
        /// </summary>
        public string OwnerId { get; set; }

    }
}
