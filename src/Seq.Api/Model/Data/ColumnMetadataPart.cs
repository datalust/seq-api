using Newtonsoft.Json;

namespace Seq.Api.Model.Data
{
    /// <summary>
    /// The metadata that can be reported for a column.
    /// </summary>
    public class ColumnMetadataPart
    {
        /// <summary>
        /// The error value for a column that is a `Bucket` expression.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? BucketErrorParameter { get; set; }
        
        /// <summary>
        /// The time grouping in ticks.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? IntervalTicks { get; set; }
    }
}