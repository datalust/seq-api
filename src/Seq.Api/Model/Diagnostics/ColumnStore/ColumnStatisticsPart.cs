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
namespace Seq.Api.Model.Diagnostics.ColumnStore;

/// <summary>
/// Describes a particular (column name, encoding) pair. Storage for each column is broken down by encoding because
/// the summary information available for each encoding differs.
/// </summary>
public class ColumnStatisticsPart
{
    /// <summary>
    /// The name of the column. For most user-provided data, such as metrics or labels, the column name will
    /// correspond to the metric or label name. Complex objects are decomposed into individual columns per sub-property.
    /// Note that these names approximate the Seq query language syntax for referring to the column, but this is
    /// inexact.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// A textual description of the native encoding scheme used for the column data.
    /// </summary>
    public string Encoding { get; set; }
   
    /// <summary>
    /// The number of rows in which the column has a defined value.
    /// </summary>
    public long Count { get; set; }
    
    /// <summary>
    /// The full on-disk size of the encoded column data, excluding framing details such as CRC blocks, padding, and
    /// file layout overhead (these are usually trivial).
    /// </summary>
    public long SizeBytes { get; set; }
}
