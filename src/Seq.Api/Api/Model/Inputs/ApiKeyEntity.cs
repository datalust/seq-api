using System.Collections.Generic;
using Seq.Api.Model.LogEvents;
using Seq.Api.Model.Shared;

namespace Seq.Api.Model.Inputs
{
    public class ApiKeyEntity : Entity
    {
        public ApiKeyEntity()
        {
            AppliedProperties = new List<InputAppliedPropertyPart>();
            AppliedFilter = new EventFilterPart();
            Metrics = new ApiKeyMetricsPart();
        }

        public string Title { get; set; }
        public string Token { get; set; }
        public List<InputAppliedPropertyPart> AppliedProperties { get; set; }
        public EventFilterPart AppliedFilter { get; set; }
        public bool CanActAsPrincipal { get; set; }
        public LogEventLevel? MinimumLevel { get; set; }
        public int InfluxPerMinute { get; set; }
        public int ArrivalsPerMinute { get; set; }
        public ApiKeyMetricsPart Metrics { get; set; }
    }
}
