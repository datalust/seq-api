    namespace Seq.Api.Model.Indexes
{
    /// <summary>
    /// An index over the event stream. May be one of several types discriminated by <see cref="IndexedEntityType"/>.
    /// </summary>
    public class IndexEntity: Entity
    {
        /// <summary>
        /// The `Id` of the associated entity (Signal, Alert or Expression index).
        /// </summary>
        public string IndexedEntityId { get; set; }
        
        /// <summary>
        /// The type of this index.
        /// </summary>
        public IndexedEntityType IndexedEntityType { get; set; }

        /// <summary>
        /// The owner / creator of this index.
        /// </summary>
        public string OwnerUsername { get; set; }

        /// <summary>
        /// The name of this index. May not be applicable to all index types.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The storage used by this index.
        /// </summary>
        public ulong StorageBytes { get; set; }
    }
}
