#nullable enable
namespace Seq.Api.Model.Metrics;

/// <summary>
/// A filter (requirement) applied to a metric dimension.
/// </summary>
public class DimensionFilterPart
{
    /// <summary>
    /// The dimension. See <see cref="MetricDimensionPart.Accessor"/>.
    /// </summary>
    public required string Accessor { get; set; }

    /// <summary>
    /// How the filter applies to the dimension.
    /// </summary>
    public DimensionFilterOperator Operator { get; set; }
    
    /// <summary>
    /// When <see cref="Operator"/> is <see cref="DimensionFilterOperator.Include"/> or
    /// <see cref="DimensionFilterOperator.Exclude"/>, the value included or excluded by the filter. Ignored otherwise.
    /// </summary>
    public object? Value { get; set; }
}