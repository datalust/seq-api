using System;

namespace Seq.Api.Model.Retention
{
    public class RetentionPolicyEntity : Seq.Api.Model.Entity
    {
        public TimeSpan RetentionTime { get; set; }

        public string ViewId { get; set; }
    }
}
