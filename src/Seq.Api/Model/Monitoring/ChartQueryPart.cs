using System.Collections.Generic;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Monitoring
{
    public class ChartQueryPart
    {
        public string Id { get; set; }
        public List<MeasurementPart> Measurements { get; set; } = new List<MeasurementPart>();
        public string Where { get; set; }
        public SignalExpressionPart SignalExpression { get; set; }
        public List<string> GroupBy { get; set; } = new List<string>();
        public MeasurementDisplayStylePart DisplayStyle { get; set; } = new MeasurementDisplayStylePart();
        public List<AlertPart> Alerts { get; set; } = new List<AlertPart>();
        public string Having { get; set; }
        public List<string> OrderBy { get; set; } = new List<string>();
        public int? Limit { get; set; }
    }
}
