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

using Newtonsoft.Json;

namespace Seq.Api.Model.Metrics;

/// <summary>
/// A description of a dimension (property/label, scope, or resource attribute) applied to metric samples.
/// </summary>
public class MetricDimensionPart
{    
    /// <summary>
    /// The dimension's accessor path, including the namespace, and using indexer syntax to refer to path elements that
    /// are not valid identifiers in the Seq query language. This member is guaranteed to contain a syntactically-valid
    /// expression that can be used directly in queries without manipulation.
    /// </summary>
    public string Accessor { get; set; }

    /// <summary>
    /// The dimension's human-readable name, without namespace qualifiers such as <c>@Resource</c>, and potentially
    /// including syntactically-invalid elements. Omitted if identical to the value of <see cref="Accessor"/>.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Name { get; set; }

    /// <summary>
    /// The dimension's namespace. Omitted if the namespace is the default <c>@Properties</c>.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Namespace { get; set; }
}
