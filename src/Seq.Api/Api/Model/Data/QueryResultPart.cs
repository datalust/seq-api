using Newtonsoft.Json;

namespace Seq.Api.Model.Data
{
    public class QueryResultPart
    {
        public string[] Columns { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object[][] Rows { get; set; }
        // or
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TimeSlicePart[] Slices { get; set; }
        // or
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TimeseriesPart[] Series { get; set; }

        // On error only:

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Error { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] Reasons { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Suggestion { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public QueryExecutionStatisticsPart Statistics { get; set; }
    }
}