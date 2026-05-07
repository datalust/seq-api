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

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Metrics;
using Seq.Api.Model.Shared;
namespace Seq.Api.ResourceGroups;

/// <summary>
/// Retrieve information about the metrics stored in this Seq instance.
/// </summary>
public class MetricsResourceGroup : ApiResourceGroup
{
    internal MetricsResourceGroup(ILoadResourceGroup connection)
        : base("Metrics", connection)
    {
    }
    
    /// <summary>
    /// Retrieve metric definitions for samples matching a set of search criteria.
    /// </summary>
    /// <param name="groups">Expressions naming any properties that determine the uniqueness/identity of metric definitions.</param>
    /// <param name="filter">A strict Seq filter expression to match (text expressions must be in double quotes). To
    /// convert a "fuzzy" filter into a strict one the way the Seq UI does, use <see cref="ExpressionsResourceGroup.ToStrictAsync"/>.</param>
    /// <param name="count">The number of definitions to retrieve. If not specified will default to 30.</param>
    /// <param name="rangeStartUtc">The earliest timestamp from which to include events in the query result.</param>
    /// <param name="rangeEndUtc">The exclusive latest timestamp to which events are included in the query result. The default is the current time.</param>
    /// <param name="timeout">The query timeout; if not specified, the query will run until completion.</param>
    /// <param name="variables">Values for any free variables that appear in <paramref name="filter"/>.</param>
    /// <param name="trace">Enable detailed (server-side) query tracing.</param>
    /// <param name="cancellationToken">Token through which the operation can be cancelled.</param>
    /// <returns>A structured result set.</returns>
    public async Task<MetricSearchResultPart> SearchAsync(
        List<string> groups = null,
        string filter = null,
        int count = 30,
        DateTime? rangeStartUtc = null,
        DateTime? rangeEndUtc = null,
        TimeSpan? timeout = null,
        Dictionary<string, object> variables = null,
        bool trace = false,
        CancellationToken cancellationToken = default)
    {
        var parameters = MakeParameters(groups, filter, count, rangeStartUtc, rangeEndUtc, timeout, trace);
        var body = new EvaluationContextPart { Variables = variables };
        return await GroupPostAsync<EvaluationContextPart, MetricSearchResultPart>("Search", body, parameters, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve information about the labels available for filtering samples matching a set of search criteria.
    /// </summary>
    /// <param name="count">The number of definitions to retrieve. If not specified will default to 30.</param>
    /// <param name="metric">Optionally, the name of a metric to limit dimension search to. By default, dimensions
    /// for all metrics are returned.</param>
    /// <param name="rangeStartUtc">The earliest timestamp from which to include events in the query result.</param>
    /// <param name="rangeEndUtc">The exclusive latest timestamp to which events are included in the query result. The default is the current time.</param>
    /// <param name="timeout">The query timeout; if not specified, the query will run until completion.</param>
    /// <param name="trace">Enable detailed (server-side) query tracing.</param>
    /// <param name="cancellationToken">Token through which the operation can be cancelled.</param>
    /// <returns>A structured result set.</returns>
    public async Task<List<MetricDimensionPart>> ListDimensionsAsync(
        int count = 30,
        string metric  = null,
        DateTime? rangeStartUtc = null,
        DateTime? rangeEndUtc = null,
        TimeSpan? timeout = null,
        bool trace = false,
        CancellationToken cancellationToken = default)
    {
        var parameters = MakeParameters(null, null, count, rangeStartUtc, rangeEndUtc, timeout, trace);
        if (metric != null)
            parameters.Add(nameof(metric), metric);
        var body = new EvaluationContextPart();
        return await GroupPostAsync<EvaluationContextPart, List<MetricDimensionPart>>("Dimensions", body, parameters, cancellationToken).ConfigureAwait(false);
    }

    static Dictionary<string, object> MakeParameters(List<string> groups, string filter, int count, DateTime? rangeStartUtc, DateTime? rangeEndUtc, TimeSpan? timeout, bool trace)
    {
        var parameters = new Dictionary<string, object>
        {
            [nameof(count)] = count
        };

        if (groups?.Count > 0)
            parameters.Add(nameof(groups), string.Join(",", groups));
        
        if (!string.IsNullOrWhiteSpace(filter))
            parameters.Add(nameof(filter), filter);

        if (rangeStartUtc != null)
            parameters.Add(nameof(rangeStartUtc), rangeStartUtc);

        if (rangeEndUtc != null)
            parameters.Add(nameof(rangeEndUtc), rangeEndUtc.Value);
        
        if (timeout != null)
            parameters.Add("timeoutMS", timeout.Value.TotalMilliseconds.ToString("0"));
            
        if (trace)
            parameters.Add(nameof(trace), true);
        return parameters;
    }
}
