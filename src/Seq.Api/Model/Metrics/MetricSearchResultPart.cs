using System.Collections.Generic;

#nullable enable

namespace Seq.Api.Model.Metrics;

/// <summary>
/// The result of a metric search.
/// </summary>
public class MetricSearchResultPart
{
    /// <summary>
    /// The metrics matched by the search.
    /// </summary>
    public List<MetricPart> Metrics { get; set; } = [];

    /// <summary>
    /// The subset of each included metric's <see cref="MetricPart.Condition"/> not derived
    /// from <see cref="MetricPart.GroupKey"/>, if any. Note that this does not
    /// need to be included when retrieving samples for the metric: this field is supplied so that additional queries
    /// can be generated utilizing the same data condition in the way already facilitated for groupings by
    /// the <see cref="MetricPart.GroupKey"/> property.
    /// </summary>
    public string? NonGroupingCondition { get; set; }
}