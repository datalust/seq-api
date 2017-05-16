using System.Collections.Generic;

namespace Seq.Api.Model.Monitoring
{
    public class ChartQueryPart
    {
        public string Id { get; set; }
        public List<MeasurementPart> Measurements { get; set; } = new List<MeasurementPart>();
        public string Where { get; set; }
        public List<string> SignalIds { get; set; } = new List<string>();
        public List<string> GroupBy { get; set; } = new List<string>();
        public MeasurementDisplayStylePart DisplayStyle = new MeasurementDisplayStylePart();
        public List<AlertPart> Alerts { get; set; } = new List<AlertPart>();
    }
}
