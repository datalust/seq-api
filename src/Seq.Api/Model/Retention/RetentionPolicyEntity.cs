using System;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Retention
{
    public class RetentionPolicyEntity : Entity
    {
        public TimeSpan RetentionTime { get; set; }

        public SignalExpressionPart RemovedSignalExpression { get; set; }

        [Obsolete("Replaced by RemovedSignalExpression.")]
        public string SignalId { get; set; }
    }
}
