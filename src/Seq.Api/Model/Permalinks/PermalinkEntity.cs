using System;
using Newtonsoft.Json;
using Seq.Api.Model.Events;

namespace Seq.Api.Model.Permalinks
{
    public class PermalinkEntity : Entity
    {
        public string OwnerId { get; set; }
        public string EventId { get; set; }
        public DateTime CreatedUtc { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public EventEntity Event { get; set; }
    }
}
