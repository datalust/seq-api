using System.Collections.Generic;

namespace Seq.Api.Model.Monitoring
{
    public class DashboardEntity : Entity
    {
        public string OwnerId { get; set; }

        public string Title { get; set; }

        public List<string> SignalIds { get; set; } = new List<string>();

        public List<ChartPart> Charts { get; set; } = new List<ChartPart>();
    }
}
