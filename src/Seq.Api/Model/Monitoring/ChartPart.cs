using System.Collections.Generic;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Monitoring
{
    public class ChartPart
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public SignalExpressionPart SignalExpression { get; set; }
        public List<ChartQueryPart> Queries { get; set; } = new List<ChartQueryPart>();
        public ChartDisplayStylePart DisplayStyle { get; set; } = new ChartDisplayStylePart();
    }
}
