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

#nullable enable
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Seq.Api.Model.Metrics;

/// <summary>
/// The definition of a metric present in the server's <c>series</c> metrics store.
/// </summary>
/// <remarks>
/// Because Seq's internal metric store uses a columnar, rather than timeseries, architecture, it does not impose a
/// fixed notion of metric identity. Instead, metric identity can be flexibly defined in the context of a metrics search.
/// A <see cref="MetricPart"/> is the result of such a search, and carries the implied metric identity in its combination
/// of <see cref="Accessor"/>, <see cref="Kind"/>, <see cref="Unit"/>, and <see cref="GroupKey"/> fields.  
/// </remarks>
public class MetricPart
{
    /// <summary>
    /// The name of the metric, as a property accessor expression, using indexer syntax to refer to path elements that
    /// are not valid identifiers in the Seq query language. This member is guaranteed to contain a syntactically-valid
    /// expression that can be used directly in queries without manipulation.
    /// </summary>
    public required string Accessor { get; set; }

    /// <summary>
    /// The metric's human-readable name, without namespace qualifiers such as <c>@Resource</c>, and potentially
    /// including syntactically-invalid elements. Omitted if identical to the value of <see cref="Accessor"/>.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Name { get; set; }

    /// <summary>
    /// The group within which this metric appears. The indexes into the array will correspond to the indexes of
    /// the list of group key expressions provided when retrieving the definition. For example, if the definitions
    /// are retrieved with <c>?group=@Resource.service.name,@Scope.name</c>, then the contents of a returned group
    /// key might resemble <c>["users_api", "AspNetCore.Server.Kestrel"]</c>.
    /// </summary>
    public List<object?> GroupKey { get; set; } = [];
    
    /// <summary>
    /// The type of data this metric records. This will typically be one of either
    /// <c>Sum</c>, <c>Gauge</c>, or (exponential) <c>Histogram</c>.
    /// </summary>
    public required string Kind { get; set; }
    
    /// <summary>
    /// A user-facing description of the metric, if one is supplied.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// The unit of measure for the metric samples, if one is supplied.
    /// </summary>
    public string? Unit { get; set; }
    
    /// <summary>
    /// If a filter or groupings were supplied when retrieving this metric, a <c>where</c>-clause compatible expression
    /// that restricts queries for metric data to only the samples matched in the filter, or belonging to the group.
    /// </summary>
    public string? Condition { get; set; }
}
