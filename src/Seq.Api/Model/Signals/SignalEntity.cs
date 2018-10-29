using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Seq.Api.Model.Signals
{
    public class SignalEntity : Entity
    {
        public SignalEntity()
        {
            Title = "New Signal";
            Filters = new List<SignalFilterPart>();
            TaggedProperties = new List<TaggedPropertyPart>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<SignalFilterPart> Filters { get; set; }

        public List<TaggedPropertyPart> TaggedProperties { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        // ReSharper disable once UnusedMember.Global
        [Obsolete("This member has been renamed `IsProtected` to better reflect its purpose.")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? IsRestricted { get; set; }

        public bool IsProtected { get; set; }

        public SignalGrouping Grouping { get; set; }

        public string ExplicitGroupName { get; set; }

        public string OwnerId { get; set; }
    }
}
