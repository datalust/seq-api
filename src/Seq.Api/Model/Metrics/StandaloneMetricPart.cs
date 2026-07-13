#nullable enable
using System.Collections.Generic;
using Newtonsoft.Json;
using Seq.Api.Model.Shared;

namespace Seq.Api.Model.Metrics;

/// <summary>
/// A <see cref="Metric"/> captured for computation and display outside the context of the original search result in
/// which it appears.
/// </summary>
public class StandaloneMetricPart
{
    /// <summary>
    /// The metric.
    /// </summary>
    public required MetricPart Metric { get; set; }

    /// <summary>
    /// The expressions used to compute the metric's <see cref="MetricPart.GroupKey"/> values. The
    /// <see cref="GroupKeyExpressions"/> and <see cref="MetricPart.GroupKey"/> values are combined with
    /// <see cref="NonGroupingCondition"/> in the metric's <see cref="MetricPart.Condition"/>.
    /// </summary>
    /// <remarks>
    /// When an ephemeral metric search result is pinned, the group keys and conditions used when retrieving it
    /// are "frozen", and attached to the pinned metric.
    /// </remarks>
    public List<string> GroupKeyExpressions { get; set; } = [];
    
    /// <summary>
    /// The non-grouping portion of <see cref="MetricPart.Condition"/>. The metric's <see cref="MetricPart.Kind"/>,
    /// <see cref="MetricPart.Unit"/>, 
    /// <see cref="GroupKeyExpressions"/> and <see cref="MetricPart.GroupKey"/> values are combined with
    /// <see cref="NonGroupingCondition"/> in the metric's <see cref="MetricPart.Condition"/>.
    /// </summary>
    /// <remarks>
    /// When an ephemeral metric search result is pinned, the group keys and conditions used when retrieving it
    /// are "frozen", and attached to the pinned metric.
    /// </remarks>
    public string? NonGroupingCondition { get; set; }
    
    /// <summary>
    /// When displaying the metric, whether the y-axis should use logarithmic tick intervals. If <c langword="false"/>,
    /// the y-axis ticks will be at intervals (linear).
    /// </summary>
    public bool UseLogarithmicScale { get; set; }
    
    /// <summary>
    /// When displaying the metric, whether the y-axis should begin from zero. If <c langword="false"/>, the y-axis
    /// will begin at the value of the smallest data point.
    /// </summary>
    public bool ShowZero { get; set; } = true;
    
    /// <summary>
    /// When displaying the metric, the aggregate function(s) used when aggregating a metric by time interval.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MetricAggregationPreference? AggregationPreference { get; set; }

    /// <summary>
    /// When displaying the metric, additional filters applied to the underlying data.
    /// </summary>
    public List<DescriptiveFilterPart> DisplayExpressionFilters { get; set; } = [];
    
    /// <summary>
    /// When displaying the metric, additional keys used to create on-chart group series.
    /// </summary>
    public List<string> DisplayGroupKeyExpressions { get; set; } = [];
    
    /// <summary>
    /// When displaying the metric, additional dimension-based filters applied to the underlying data.
    /// </summary>
    public List<DimensionFilterPart> DisplayDimensionFilters { get; set; } = [];
}