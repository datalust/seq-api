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

namespace Seq.Api.Model.Diagnostics.ColumnStore;

/// <summary>
/// Provides information about the internal column store that drives Seq's metrics features.
/// </summary>
public class ColumnStoreStatisticsPart
{
    /// <summary>
    /// The columns stored in the column store.
    /// </summary>
    public List<ColumnStatisticsPart> Columns { get; set; } = new();
}