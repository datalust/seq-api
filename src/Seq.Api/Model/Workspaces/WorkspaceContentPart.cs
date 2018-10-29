using System.Collections.Generic;

namespace Seq.Api.Model.Workspaces
{
    public class WorkspaceContentPart
    {
        public List<string> SignalIds { get; set; } = new List<string>();
        public List<string> QueryIds { get; set; } = new List<string>();
        public List<string> DashboardIds { get; set; } = new List<string>();
    }
}