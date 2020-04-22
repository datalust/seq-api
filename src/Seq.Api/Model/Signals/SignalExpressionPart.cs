// Copyright © Datalust and contributors. 
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Seq.Api.Model.Signals
{
    /// <summary>
    /// A signal expression combines one or more signals to identify a subset of events.
    /// </summary>
    public class SignalExpressionPart
    {
        /// <summary>
        /// The kind of this expression. Signal expressions form a tree of nodes; the <see cref="Kind"/>
        /// determines what kind of node this expression is, and therefore which other properties
        /// of the <see cref="SignalExpressionPart"/> are valid.
        /// </summary>
        public SignalExpressionKind Kind { get; set; }

        /// <summary>
        /// When <see cref="Kind"/> is <see cref="SignalExpressionKind.Signal"/>, the id of the
        /// signal represented by this expression node.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SignalId { get; set; }

        /// <summary>
        /// When <see cref="Kind"/> is <see cref="SignalExpressionKind.Intersection"/> or
        /// <see cref="SignalExpressionKind.Union"/>, the left side of the operation.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SignalExpressionPart Left { get; set; }

        /// <summary>
        /// When <see cref="Kind"/> is <see cref="SignalExpressionKind.Intersection"/> or
        /// <see cref="SignalExpressionKind.Union"/>, the right side of the operation.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SignalExpressionPart Right { get; set; }

        /// <summary>
        /// Create a <see cref="SignalExpressionPart"/> representing a single signal.
        /// </summary>
        /// <param name="signalId">The id of the signal.</param>
        /// <returns>The equivalent expression.</returns>
        public static SignalExpressionPart Signal(string signalId)
        {
            if (signalId == null) throw new ArgumentNullException(nameof(signalId));
            return new SignalExpressionPart {Kind = SignalExpressionKind.Signal, SignalId = signalId};
        }

        /// <summary>
        /// Create a <see cref="SignalExpressionPart"/> representing an intersection (<c>and</c> operation)
        /// of two signal expressions.
        /// </summary>
        /// <param name="left">The left side of the operation.</param>
        /// <param name="right">The right side of the operation.</param>
        /// <returns>The equivalent expression.</returns>
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

        /// <summary>
        /// Create a <see cref="SignalExpressionPart"/> representing an union (<c>or</c> operation)
        /// of two signal expressions.
        /// </summary>
        /// <param name="left">The left side of the operation.</param>
        /// <param name="right">The right side of the operation.</param>
        /// <returns>The equivalent expression.</returns>
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

        /// <summary>
        /// Create a <see cref="SignalExpressionPart"/> representing an intersection (<c>and</c> operation)
        /// of multiple signals.
        /// </summary>
        /// <param name="intersectIds">The ids of the intersected signals.</param>
        /// <returns>The equivalent expression.</returns>
        public static SignalExpressionPart FromIntersectedIds(IEnumerable<string> intersectIds)
        {
            if (intersectIds == null) throw new ArgumentNullException(nameof(intersectIds));

            if (!intersectIds.Any())
                return null;

            var first = Signal(intersectIds.First());
            return intersectIds.Skip(1).Aggregate(first, (lhs, rhs) => Intersection(lhs, Signal(rhs)));
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            if (Kind == SignalExpressionKind.Signal)
                return SignalId;

            if (Kind == SignalExpressionKind.Intersection)
                return $"{Group(Left)},{Group(Right)}";

            if (Kind == SignalExpressionKind.Union)
                return $"{Group(Left)}~{Group(Right)}";

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
