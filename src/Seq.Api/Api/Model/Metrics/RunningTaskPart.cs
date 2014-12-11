using System;

namespace Seq.Api.Model.Metrics
{
    public class RunningTaskPart
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime StartedAtUtc { get; set; }
    }
}