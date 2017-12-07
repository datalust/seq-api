using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Seq.Api.Model.Signals
{
    public class SignalExpressionPart
    {
        public SignalExpressionKind Kind { get; set; }

        // SignalExpressionKind.Signal

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SignalId { get; set; }

        // SignalExpressionKind.Intersection, SignalExpressionKind.Union

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SignalExpressionPart Left { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SignalExpressionPart Right { get; set; }

        public static SignalExpressionPart Signal(string signalId)
        {
            if (signalId == null) throw new ArgumentNullException(nameof(signalId));
            return new SignalExpressionPart {Kind = SignalExpressionKind.Signal, SignalId = signalId};
        }

        public static SignalExpressionPart Intersection(SignalExpressionPart left, SignalExpressionPart right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            return new SignalExpressionPart
            {
                Kind = SignalExpressionKind.Intersection,
                Left = left,
                Right = right
            };
        }

        public static SignalExpressionPart Union(SignalExpressionPart left, SignalExpressionPart right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            return new SignalExpressionPart
            {
                Kind = SignalExpressionKind.Union,
                Left = left,
                Right = right
            };
        }

        public static SignalExpressionPart FromIntersectedIds(IEnumerable<string> intersectIds)
        {
            if (intersectIds == null) throw new ArgumentNullException(nameof(intersectIds));

            if (!intersectIds.Any())
                return null;

            var first = Signal(intersectIds.First());
            return intersectIds.Skip(1).Aggregate(first, (lhs, rhs) => Intersection(lhs, Signal(rhs)));
        }

        public override string ToString()
        {
            if (Kind == SignalExpressionKind.Signal)
                return SignalId;

            if (Kind == SignalExpressionKind.Intersection)
                return $"{Group(Left)},{Group(Right)}";

            if (Kind == SignalExpressionKind.Union)
                return $"{Group(Left)}+{Group(Right)}";

            throw new InvalidOperationException("Invalid signal expression kind.");
        }

        static string Group(SignalExpressionPart signalExpression)
        {
            if (signalExpression.Kind == SignalExpressionKind.Signal)
                return signalExpression.ToString();

            return $"({signalExpression})";
        }
    }
}
