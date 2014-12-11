using System.Collections.Generic;
using Seq.Api.Model.Shared;

namespace Seq.Api.Model.Events
{
    public class ResultSetPart
    {
        public List<EventEntity> Events { get; set; }
        public StatisticsPart Statistics { get; set; }
    }
}
