namespace Seq.Api.Model.Shared
{
    /// <summary>
    /// A column appearing within a group-by clause.
    /// </summary>
    public class GroupingColumnPart
    {
        /// <summary>
        /// The expression (<c>select</c>ed column) that computes the value of the measurement.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// An optional label for the measurement (effectively the right-hand size of an <c>as</c> clause).
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// If <c>true</c>, the grouping is case-insensitive; otherwise, the grouping will be case-sensitive.
        /// </summary>
        public bool IsCaseInsensitive { get; set; }
    }
}
