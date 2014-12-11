using System;

namespace Seq.Api.Model.Watches
{
    public class WatchEntity : Seq.Api.Model.Entity
    {
        public string OwnerId { get; set; }
        public string Description { get; set; }
        public string ViewId { get; set; }
        public string QueryId { get; set; }
        public string Filter { get; set; }
        public long CountSinceReset { get; set; }
        public DateTime ResetAtUtc { get; set; }
        public string DisplayStyle { get; set; }
    }
}
