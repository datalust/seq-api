namespace Seq.Api.Model.Metrics;

/// <summary>
/// The aggregate function(s) used when aggregating a metric by time interval for display.
/// </summary>
public enum MetricAggregationPreference
{
    /// <summary>
    /// The <c>count()</c> aggregate function.
    /// </summary>
    Count,

    /// <summary>
    /// The <c>sum()</c> aggregate function.
    /// </summary>
    Sum,
    
    /// <summary>
    /// The <c>min()</c> aggregate function.
    /// </summary>
    Min,
    
    /// <summary>
    /// The <c>mean()</c> aggregate function.
    /// </summary>
    Mean,
    
    /// <summary>
    /// The <c>max()</c> aggregate function.
    /// </summary>
    Max
}