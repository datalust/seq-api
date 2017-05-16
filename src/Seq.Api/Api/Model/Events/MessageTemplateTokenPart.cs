using Newtonsoft.Json;

namespace Seq.Api.Model.Events
{
    public class MessageTemplateTokenPart
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        // or
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RawText { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FormattedValue { get; set; }
    }
}
