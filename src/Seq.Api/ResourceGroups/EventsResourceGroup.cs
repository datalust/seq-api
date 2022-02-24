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
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Events;
using Seq.Api.Model.Shared;
using Seq.Api.Model.Signals;
using Seq.Api.Streams;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Read and subscribe to events from the event store.
    /// </summary>
    public class EventsResourceGroup : ApiResourceGroup
    {
        internal EventsResourceGroup(ILoadResourceGroup connection)
            : base("Events", connection)
        {
        }

        /// <summary>Find an event, given its id.</summary>
        /// <param name="id">The id of the event to retrieve.</param>
        /// <param name="render">If specified, the event's message template and properties will be rendered into its RenderedMessage property.</param>
        /// <param name="permalinkId">If the request is for a permalinked event, specifying the id of the permalink here will
        /// allow events that have otherwise been deleted to be found. The special value `"unknown"` provides backwards compatibility
        /// with versions prior to 5.0, which did not mark permalinks explicitly.</param>
        /// <param name="cancellationToken">Token through which the operation can be cancelled.</param>
        /// <returns>The event.</returns>
        public async Task<EventEntity> FindAsync(
            string id,
            bool render = false,
            string permalinkId = null,
            CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var parameters = new Dictionary<string, object> {{"id", id}};
            if (render) parameters.Add("render", true);
            if (permalinkId != null) parameters.Add("permalinkId", permalinkId);

            return await GroupGetAsync<EventEntity>("Item", parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Efficiently retrieve all events that match a set of conditions.
        /// </summary>
        /// <param name="unsavedSignal">A constructed signal that may not appear on the server, for example, a <see cref="SignalEntity"/> that has been
        /// created but not saved, a signal from another server, or the modified representation of an entity already persisted.</param>
        /// <param name="signal">If provided, a signal expression describing the set of events that will be filtered for the result.</param>
        /// <param name="filter">A strict Seq filter expression to match (text expressions must be in double quotes). To
        /// convert a "fuzzy" filter into a strict one the way the Seq UI does, use connection.Expressions.ToStrictAsync().</param>
        /// <param name="count">The number of events to retrieve. If not specified will default to 30.</param>
        /// <param name="startAtId">An event id from which to start searching (inclusively).</param>
        /// <param name="afterId">An event id to search after (exclusively).</param>
        /// <param name="render">If specified, the event's message template and properties will be rendered into its RenderedMessage property.</param>
        /// <param name="fromDateUtc">Earliest (inclusive) date/time from which to search.</param>
        /// <param name="toDateUtc">Latest (exclusive) date/time from which to search.</param>
        /// <param name="shortCircuitAfter">If specified, the number of events after the first match to keep searching before a partial
        /// result set is returned. Used to improve responsiveness when the result is displayed in a user interface, not typically used in
        /// batch processing scenarios.</param>
        /// <param name="permalinkId">If the request is for a permalinked event, specifying the id of the permalink here will
        /// allow events that have otherwise been deleted to be found. The special value `"unknown"` provides backwards compatibility
        /// with versions prior to 5.0, which did not mark permalinks explicitly.</param>
        /// <param name="cancellationToken">Token through which the operation can be cancelled.</param>
        /// <param name="variables">Values for any free variables that appear in <paramref name="filter"/>.</param>
        /// <returns>The complete list of events, ordered from least to most recent.</returns>
        public async IAsyncEnumerable<EventEntity> EnumerateAsync(
            SignalEntity unsavedSignal = null,
            SignalExpressionPart signal = null,
            string filter = null,
            int count = 30,
            string startAtId = null,
            string afterId = null,
            bool render = false,
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null,
            int? shortCircuitAfter = null,
            string permalinkId = null,
            Dictionary<string, object> variables = null,
            [EnumeratorCancellation]
            CancellationToken cancellationToken = default)
        {
            // Limit server resource consumption. Assuming events are between 1000 and 10,000 bytes each,
            // responses will be between 5 and 50 MB.
            const int pageSize = 5000;
            
            var nextAfterId = afterId;
            var remaining = count;
            var nextCount = Math.Min(remaining, pageSize);

            while (true)
            {
                var resultSet = await PageAsync(unsavedSignal, signal, filter, nextCount, startAtId, nextAfterId, render,
                    fromDateUtc, toDateUtc, shortCircuitAfter, permalinkId, variables, cancellationToken).ConfigureAwait(false);

                foreach (var evt in resultSet.Events)
                {
                    yield return evt;
                    remaining -= 1;

                    if (remaining <= 0)
                        yield break;
                }

                if (remaining <= 0)
                    yield break;

                if (resultSet.Statistics.Status == ResultSetStatus.Complete)
                    yield break;
                
                nextAfterId = resultSet.Statistics.LastReadEventId;
                nextCount = Math.Min(remaining, pageSize);
            }
        }

        
        /// <summary>
        /// Retrieve all events that match a set of conditions. The complete result is buffered into memory,
        /// so if a large result set is expected, use <c>EnumerateAsync()</c>, or <c>PageAsync()</c> with
        /// <paramref name="count" /> and <paramref name="afterId"/> to page the results.
        /// </summary>
        /// <param name="unsavedSignal">A constructed signal that may not appear on the server, for example, a <see cref="SignalEntity"/> that has been
        /// created but not saved, a signal from another server, or the modified representation of an entity already persisted.</param>
        /// <param name="signal">If provided, a signal expression describing the set of events that will be filtered for the result.</param>
        /// <param name="filter">A strict Seq filter expression to match (text expressions must be in double quotes). To
        /// convert a "fuzzy" filter into a strict one the way the Seq UI does, use connection.Expressions.ToStrictAsync().</param>
        /// <param name="count">The number of events to retrieve. If not specified will default to 30.</param>
        /// <param name="startAtId">An event id from which to start searching (inclusively).</param>
        /// <param name="afterId">An event id to search after (exclusively).</param>
        /// <param name="render">If specified, the event's message template and properties will be rendered into its RenderedMessage property.</param>
        /// <param name="fromDateUtc">Earliest (inclusive) date/time from which to search.</param>
        /// <param name="toDateUtc">Latest (exclusive) date/time from which to search.</param>
        /// <param name="shortCircuitAfter">If specified, the number of events after the first match to keep searching before a partial
        /// result set is returned. Used to improve responsiveness when the result is displayed in a user interface, not typically used in
        /// batch processing scenarios.</param>
        /// <param name="permalinkId">If the request is for a permalinked event, specifying the id of the permalink here will
        /// allow events that have otherwise been deleted to be found. The special value `"unknown"` provides backwards compatibility
        /// with versions prior to 5.0, which did not mark permalinks explicitly.</param>
        /// <param name="variables">Values for any free variables that appear in <paramref name="filter"/>.</param>
        /// <param name="cancellationToken">Token through which the operation can be cancelled.</param>
        /// <returns>The result set with a page of events.</returns>
        public async Task<List<EventEntity>> ListAsync(
            SignalEntity unsavedSignal = null,
            SignalExpressionPart signal = null,
            string filter = null,
            int count = 30,
            string startAtId = null,
            string afterId = null,
            bool render = false,
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null,
            int? shortCircuitAfter = null,
            string permalinkId = null,
            Dictionary<string, object> variables = null,
            CancellationToken cancellationToken = default)
        {
            var results = new List<EventEntity>();
            await foreach (var item in EnumerateAsync(unsavedSignal, signal, filter, count, startAtId, afterId, render,
                                   fromDateUtc, toDateUtc, shortCircuitAfter, permalinkId, variables, cancellationToken)
                           .WithCancellation(cancellationToken)
                           .ConfigureAwait(false))
                results.Add(item);
            return results;   
        }

        /// <summary>
        /// Retrieve a page of events that match a set of conditions.
        /// </summary>
        /// <param name="unsavedSignal">A constructed signal that may not appear on the server, for example, a <see cref="SignalEntity"/> that has been
        /// created but not saved, a signal from another server, or the modified representation of an entity already persisted.</param>
        /// <param name="signal">If provided, a signal expression describing the set of events that will be filtered for the result.</param>
        /// <param name="filter">A strict Seq filter expression to match (text expressions must be in double quotes). To
        /// convert a "fuzzy" filter into a strict one the way the Seq UI does, use connection.Expressions.ToStrictAsync().</param>
        /// <param name="count">The number of events to retrieve. If not specified will default to 30.</param>
        /// <param name="startAtId">An event id from which to start searching (inclusively).</param>
        /// <param name="afterId">An event id to search after (exclusively).</param>
        /// <param name="render">If specified, the event's message template and properties will be rendered into its RenderedMessage property.</param>
        /// <param name="fromDateUtc">Earliest (inclusive) date/time from which to search.</param>
        /// <param name="toDateUtc">Latest (exclusive) date/time from which to search.</param>
        /// <param name="shortCircuitAfter">If specified, the number of events after the first match to keep searching before a partial
        /// result set is returned. Used to improve responsiveness when the result is displayed in a user interface, not typically used in
        /// batch processing scenarios.</param>
        /// <param name="permalinkId">If the request is for a permalinked event, specifying the id of the permalink here will
        /// allow events that have otherwise been deleted to be found. The special value `"unknown"` provides backwards compatibility
        /// with versions prior to 5.0, which did not mark permalinks explicitly.</param>
        /// <param name="variables">Values for any free variables that appear in <paramref name="filter"/>.</param>
        /// <param name="cancellationToken">Token through which the operation can be cancelled.</param>
        /// <returns>The result set with a page of events.</returns>
        public async Task<ResultSetPart> PageAsync(
            SignalEntity unsavedSignal = null,
            SignalExpressionPart signal = null,
            string filter = null,
            int count = 30,
            string startAtId = null,
            string afterId = null,
            bool render = false,
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null,
            int? shortCircuitAfter = null,
            string permalinkId = null,
            Dictionary<string, object> variables = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>{{ "count", count }};
            if (signal != null) { parameters.Add("signal", signal.ToString()); }
            if (filter != null) { parameters.Add("filter", filter); }
            if (startAtId != null) { parameters.Add("startAtId", startAtId); }
            if (afterId != null) { parameters.Add("afterId", afterId); }
            if (render) { parameters.Add("render", true); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }
            if (shortCircuitAfter != null) { parameters.Add("shortCircuitAfter", shortCircuitAfter.Value); }
            if (permalinkId != null) { parameters.Add("permalinkId", permalinkId); }

            var body = new EvaluationContextPart { Signal = unsavedSignal, Variables = variables };
            return await GroupPostAsync<EvaluationContextPart, ResultSetPart>("InSignal", body, parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete matching events from the stream.
        /// </summary>
        /// <param name="unsavedSignal">A constructed signal that may not appear on the server, for example, a <see cref="SignalEntity"/> that has been
        /// created but not saved, a signal from another server, or the modified representation of an entity already persisted.</param>
        /// <param name="signal">If provided, a signal expression describing the set of events that will be filtered for the result.</param>
        /// <param name="filter">A strict Seq filter expression to match (text expressions must be in double quotes). To
        /// convert a "fuzzy" filter into a strict one the way the Seq UI does, use connection.Expressions.ToStrictAsync().</param>
        /// <param name="fromDateUtc">Earliest (inclusive) date/time from which to delete.</param>
        /// <param name="toDateUtc">Latest (exclusive) date/time from which to delete.</param>
        /// <param name="variables">Values for any free variables that appear in <paramref name="filter"/>.</param>
        /// <param name="cancellationToken">Token through which the operation can be cancelled.</param>
        /// <returns>A result carrying the count of events deleted.</returns>
        public async Task<DeleteResultPart> DeleteAsync(
            SignalEntity unsavedSignal = null,
            SignalExpressionPart signal = null,
            string filter = null,
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null,
            Dictionary<string, object> variables = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>();
            if (signal != null) { parameters.Add("signal", signal.ToString()); }
            if (filter != null) { parameters.Add("filter", filter); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }

            var body = new EvaluationContextPart { Signal = unsavedSignal, Variables = variables };
            return await GroupDeleteAsync<EvaluationContextPart, DeleteResultPart>("DeleteInSignal", body, parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Connect to the live event stream, read as strongly-typed objects. Dispose the resulting stream to disconnect.
        /// </summary>
        /// <typeparam name="T">The type into which events should be deserialized.</typeparam>
        /// <param name="signal">If provided, a signal expression describing the set of events that will be filtered for the result.</param>
        /// <param name="filter">A strict Seq filter expression to match (text expressions must be in double quotes). To
        /// convert a "fuzzy" filter into a strict one the way the Seq UI does, use connection.Expressions.ToStrictAsync().</param>
        /// <param name="cancellationToken">Token through which the operation can be cancelled.</param>
        /// <returns>An observable that will stream events from the server to subscribers. Events will be buffered server-side until the first
        /// subscriber connects, ensure at least one subscription is made in order to avoid event loss.</returns>
        /// <remarks>See <a href="https://docs.datalust.co/docs/posting-raw-events#section-compact-json-format">the Seq ingestion
        /// docs</a> for event schema information.</remarks>
        public async Task<ObservableStream<T>> StreamAsync<T>(
            SignalExpressionPart signal = null,
            string filter = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>();
            if (signal != null) { parameters.Add("signal", signal.ToString()); }
            if (filter != null) { parameters.Add("filter", filter); }

            var group = await LoadGroupAsync(cancellationToken).ConfigureAwait(false);
            return await Client.StreamAsync<T>(group, "Stream", parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Connect to the live event stream, read as raw JSON documents. Dispose the resulting stream to disconnect.
        /// </summary>
        /// <param name="signal">If provided, a signal expression describing the set of events that will be filtered for the result.</param>
        /// <param name="filter">A strict Seq filter expression to match (text expressions must be in double quotes). To
        /// convert a "fuzzy" filter into a strict one the way the Seq UI does, use connection.Expressions.ToStrictAsync().</param>
        /// <param name="cancellationToken">Token through which the operation can be cancelled.</param>
        /// <returns>An observable that will stream events from the server to subscribers. Events will be buffered server-side until the first
        /// subscriber connects, ensure at least one subscription is made in order to avoid event loss.</returns>
        /// <remarks>See <a href="https://docs.datalust.co/docs/posting-raw-events#section-compact-json-format">the Seq ingestion
        /// docs</a> for event schema information.</remarks>
        public async Task<ObservableStream<string>> StreamDocumentsAsync(
            SignalExpressionPart signal = null,
            string filter = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>();
            if (signal != null) { parameters.Add("signal", signal.ToString()); }
            if (filter != null) { parameters.Add("filter", filter); }

            var group = await LoadGroupAsync(cancellationToken).ConfigureAwait(false);
            return await Client.StreamTextAsync(group, "Stream", parameters, cancellationToken).ConfigureAwait(false);
        }
    }
}
