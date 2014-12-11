using System;

namespace Seq.Api.Model.Watches
{
    public class DataPointPart
    {
        public DateTime SliceStartUtc { get; set; }
        public long Value { get; set; }
    }
}
