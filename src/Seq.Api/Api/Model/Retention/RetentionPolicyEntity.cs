using System;

namespace Seq.Api.Model.Retention
{
    public class RetentionPolicyEntity : Entity
    {
        public TimeSpan RetentionTime { get; set; }

        public string SignalId { get; set; }
    }
}
