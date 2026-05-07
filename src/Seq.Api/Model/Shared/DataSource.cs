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

namespace Seq.Api.Model.Shared;

/// <summary>
/// Identifies the storage objects present in Seq's underlying telemetry database.
/// </summary>
public enum DataSource
{
    /// <summary>
    /// The <c>stream</c> object, which stores log events and trace spans. The <c>stream</c> object is implemented
    /// over row-oriented storage, optimized for general search and row-by-row retrieval.
    /// </summary>
    Stream,
    
    /// <summary>
    /// The <c>series</c> object, which stores metrics. The <c>series</c> object is implemented over columnar storage,
    /// optimized for aggregate queries that each touch a small number of columns.
    /// </summary>
    Series,
}