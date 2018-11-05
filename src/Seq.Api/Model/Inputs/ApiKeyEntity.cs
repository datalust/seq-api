using System.Collections.Generic;
using Newtonsoft.Json;
using Seq.Api.Model.LogEvents;
using Seq.Api.Model.Security;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Inputs
{
    public class ApiKeyEntity : Entity
    {
        public string Title { get; set; }
        public string Token { get; set; }
        public string TokenPrefix { get; set; }
        public List<InputAppliedPropertyPart> AppliedProperties { get; set; } = new List<InputAppliedPropertyPart>();
        public SignalFilterPart InputFilter { get; set; } = new SignalFilterPart();
        public LogEventLevel? MinimumLevel { get; set; }
        public bool UseServerTimestamps { get; set; }
        public bool IsDefault { get; set; }
        public string OwnerId { get; set; }
        public HashSet<Permission> AssignedPermissions { get; set; } = new HashSet<Permission>();

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ApiKeyMetricsPart Metrics { get; set; } = new ApiKeyMetricsPart();
    }
}
