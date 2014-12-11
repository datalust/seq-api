using System;
using Newtonsoft.Json;
using Seq.Api.Model.Events;
using Seq.Api.Model.Users;

namespace Seq.Api.Model.Pins
{
    public class PinEntity : Seq.Api.Model.Entity
    {
        public string OwnerId { get; set; }
        public string EventId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string PinType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string AssignedByUserId { get; set; }

        public string Notes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public EventEntity Event { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UserEntity AssignedByUser { get; set; }
    }
}
