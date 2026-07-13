namespace Seq.Api.Model.Metrics;

/// <summary>
/// The aggregate function(s) used when aggregating a metric by time interval for display.
/// </summary>
public enum MetricAggregationPreference
{
    /// <summary>
    /// The total count of observed values.
    /// </summary>
    Total,
    
    /// <summary>
    /// The sum of all observed values.
    /// </summary>
    Sum,
    
    /// <summary>
    /// The counts of values falling in each histogram bucket.
    /// </summary>
    BucketSum,
    
    /// <summary>
    /// The smallest observed value.
    /// </summary>
    Min,
    
    /// <summary>
    /// The center observed value.
    /// </summary>
    Mean,
    
    /// <summary>
    /// The largest observed value.
    /// </summary>
    Max,
    
    /// <summary>
    /// The set of values greater than a percentage of all other observed values.
    /// </summary>
    Percentiles
}