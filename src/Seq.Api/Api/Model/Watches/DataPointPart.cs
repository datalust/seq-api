using System;
using Newtonsoft.Json;

namespace Seq.Api.Model.Watches
{
    public class DataPointPart
    {
        public DateTime SliceStartUtc { get; set; }
        public long Value { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Uncached { get; set; }
    }
}
