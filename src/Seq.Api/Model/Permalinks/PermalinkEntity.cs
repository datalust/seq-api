using System;
using Newtonsoft.Json;
using Seq.Api.Model.Events;

namespace Seq.Api.Model.Permalinks
{
    public class PermalinkEntity : Entity
    {
        /// <summary>
        /// When retrieving an event that _may_ be permalinked (backwards compatibility),
        /// this hint is given by specifiying `permalinkId=unknown` in the API call.
        /// </summary>
        public const string UnknownId = "unknown";

        public string OwnerId { get; set; }
        public string EventId { get; set; }
        public DateTime CreatedUtc { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public EventEntity Event { get; set; }
    }
}
