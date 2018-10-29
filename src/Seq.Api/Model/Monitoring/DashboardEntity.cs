using System.Collections.Generic;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Monitoring
{
    public class DashboardEntity : Entity
    {
        public string OwnerId { get; set; }

        public string Title { get; set; }

        public bool IsProtected { get; set; }

        public SignalExpressionPart SignalExpression { get; set; }

        public List<ChartPart> Charts { get; set; } = new List<ChartPart>();
    }
}
