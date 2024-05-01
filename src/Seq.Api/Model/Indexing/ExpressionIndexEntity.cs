namespace Seq.Api.Model.Indexing
{
    /// <summary>
    /// An index based on an expression.
    /// </summary>
    public class ExpressionIndexEntity: Entity
    {
        /// <summary>
        /// The expression to be indexed.
        /// </summary>
        public string Expression { get; set; }
        
        /// <summary>
        /// A user-provided description of the index.
        /// </summary>
        public string Description { get; set; }
    }
}