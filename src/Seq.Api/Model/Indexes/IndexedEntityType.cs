namespace Seq.Api.Model.Indexes
{
    /// <summary>
    /// The type of the index.
    /// </summary>
    public enum IndexedEntityType
    {
        /// <summary>
        /// A predicate index for a signal expression.
        /// </summary>
        Signal,
        
        /// <summary>
        /// An expression index.
        /// </summary>
        ExpressionIndex,
        
        /// <summary>
        /// A predicate index for an alert filter.
        /// </summary>
        Alert,
    }
}