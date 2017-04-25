using System.Collections.Generic;

namespace Seq.Api.Model.Monitoring
{
    public class ChartPart
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public List<string> SignalIds { get; set; } = new List<string>();

        public List<ChartQueryPart> Queries { get; set; } = new List<ChartQueryPart>();
    }
}
