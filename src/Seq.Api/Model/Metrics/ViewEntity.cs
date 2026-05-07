#nullable enable
using System.Collections.Generic;
using Seq.Api.Model.Security;
using Seq.Api.Model.Shared;

namespace Seq.Api.Model.Metrics;

/// <summary>
/// A saved view in the Seq Metrics screen.
/// </summary>
public class ViewEntity: Entity
{
    /// <summary>
    /// Construct a <see cref="ViewEntity"/>.
    /// </summary>
    public ViewEntity()
    {
        Title = "New View";
    }
    
    /// <summary>
    /// The friendly, human-readable title of the view.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// A long-form description of the view's purpose and contents.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The user id of the user who owns the view; if <c>null</c>, the view is shared.
    /// </summary>
    public string? OwnerId { get; set; }
    
    /// <summary>
    /// If <c>true</c>, the view can only be modified by users with the <see cref="Permission.Project"/> permission.
    /// </summary>
    public bool IsProtected { get; set; }
    
    /// <summary>
    /// One or more expressions (normally, dimension accessor expressions such as <c>@Scope.name</c>) around which
    /// metrics are split into separate charts.
    /// </summary>
    public List<string> GroupKeyExpressions { get; set; } = [];
    
    /// <summary>
    /// Expressions used to restrict the underlying metric data considered by the view.
    /// </summary>
    public List<DescriptiveFilterPart> Filters { get; set; } = [];
    
    /// <summary>
    /// Per-dimension filters used to restrict the underlying metric data considered by the view.
    /// </summary>
    public List<DimensionFilterPart> DimensionFilters { get; set; } = [];
    
    /// <summary>
    /// Individual metrics pinned in the view.
    /// </summary>
    public List<StandaloneMetricPart> PinnedMetrics { get; set; } = [];
}