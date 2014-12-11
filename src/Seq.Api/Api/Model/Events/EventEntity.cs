using System.Collections.Generic;
using Newtonsoft.Json;

namespace Seq.Api.Model.Events
{
    public class EventEntity : Seq.Api.Model.Entity
    {
        public string Timestamp { get; set; }
        public List<EventPropertyPart> Properties { get; set; }
        public List<MessageTemplateTokenPart> MessageTemplateTokens { get; set; }
        public string EventType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Level { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Exception { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RenderedMessage { get; set; }
    }
}
