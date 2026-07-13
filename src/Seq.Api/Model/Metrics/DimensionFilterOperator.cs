#nullable enable
namespace Seq.Api.Model.Metrics;

/// <summary>
/// A simplified representation of the expression encoded in a <see cref="DimensionFilterPart"/>.
/// </summary>
public enum DimensionFilterOperator
{
    /// <summary>
    /// The filter includes samples with <see cref="DimensionFilterPart.Value"/> for the dimension.
    /// </summary>
    Include,

    /// <summary>
    /// The filter excludes samples with <see cref="DimensionFilterPart.Value"/> for the dimension.
    /// </summary>
    Exclude,
    
    /// <summary>
    /// The filter includes samples with any value for the dimension.
    /// </summary>
    Has,
}