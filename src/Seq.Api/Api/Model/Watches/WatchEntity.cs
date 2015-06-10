using System;
using System.Collections.Generic;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Watches
{
    public class WatchEntity : Entity
    {
        public string OwnerId { get; set; }
        public string Description { get; set; }
        public SignalFilterPart Filter { get; set; }
        public List<string> SignalIds { get; set; } 
        public long CountSinceReset { get; set; }
        public DateTime ResetAtUtc { get; set; }
        public string DisplayStyle { get; set; }
    }
}
