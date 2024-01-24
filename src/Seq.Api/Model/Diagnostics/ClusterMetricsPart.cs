namespace Seq.Api.Model.Diagnostics
{
    /// <summary>
    /// Metrics related to cluster activity.
    /// </summary>
    public class ClusterMetricsPart
    {
        /// <summary>
        /// Construct a <see cref="ClusterMetricsPart"/>.
        /// </summary>
        public ClusterMetricsPart()
        {
        }

        /// <summary>
        /// A connection to the leader node was accepted.
        /// </summary>
        public ulong ConnectionAccepted { get; set; }
        
        /// <summary>
        /// A connection to the leader node was rejected due to an invalid authentication key.
        /// </summary>
        public ulong ConnectionInvalidKey { get; set; }
        
        /// <summary>
        /// A connection to the leader node was successfully authenticated and established.
        /// </summary>
        public ulong ConnectionEstablished { get; set; }
        
        /// <summary>
        /// A connection to the leader node was could not be established.
        /// </summary>
        public ulong ConnectionNotEstablished { get; set; }
    }
}