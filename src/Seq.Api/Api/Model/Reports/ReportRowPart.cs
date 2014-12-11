using System;
using System.Collections.Generic;

namespace Seq.Api.Model.Reports
{
    public class ReportRowPart
    {
        public Guid EventId { get; set; }
        public uint EventType { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string RenderedMessage { get; set; }
        public Dictionary<string, object> Values { get; set; }
    }
}