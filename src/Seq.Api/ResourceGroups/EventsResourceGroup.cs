using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Events;
using Seq.Api.Model.Shared;
using Seq.Api.Model.Signals;
using Seq.Api.Streams;

namespace Seq.Api.ResourceGroups
{
    public class EventsResourceGroup : ApiResourceGroup
    {
        internal EventsResourceGroup(ISeqConnection connection)
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
        /// Retrieve a list of events that match a set of conditions. The complete result is buffered into memory,
        /// so if a large result set is expected, use InSignalAsync() and lastReadEventId to page the results.
        /// </summary>
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
        /// <returns>The complete list of events, ordered from least to most recent.</returns>
        public async Task<List<EventEntity>> ListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object> { { "count", count } };
            if (signal != null) { parameters.Add("signal", signal.ToString()); }
            if (filter != null) { parameters.Add("filter", filter); }
            if (startAtId != null) { parameters.Add("startAtId", startAtId); }
            if (afterId != null) { parameters.Add("afterId", afterId); }
            if (render) { parameters.Add("render", true); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }
            if (shortCircuitAfter != null) { parameters.Add("shortCircuitAfter", shortCircuitAfter.Value); }
            if (permalinkId != null) { parameters.Add("permalinkId", permalinkId); }

            var chunks = new List<List<EventEntity>>();
            var remaining = count;

            while (true)
            {
                var resultSet = await GroupGetAsync<ResultSetPart>("InSignal", parameters, cancellationToken).ConfigureAwait(false);
                chunks.Add(resultSet.Events);
                remaining -= resultSet.Events.Count;

                if (remaining <= 0)
                    break;

                if (resultSet.Statistics.Status != ResultSetStatus.Partial)
                    break;

                parameters["afterId"] = resultSet.Statistics.LastReadEventId;
                parameters["count"] = remaining;
            }

            var result = new List<EventEntity>(chunks.Sum(c => c.Count));
            foreach (var evt in chunks.SelectMany(c => c))
                result.Add(evt);

            return result;
        }

        /// <summary>
        /// Retrieve a list of events that match a set of conditions. The complete result is buffered into memory,
        /// so if a large result set is expected, use InSignalAsync() and lastReadEventId to page the results.
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
        /// <returns>The complete list of events, ordered from least to most recent.</returns>
        public async Task<ResultSetPart> InSignalAsync(
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

            var body = unsavedSignal ?? new SignalEntity();
            return await GroupPostAsync<SignalEntity, ResultSetPart>("InSignal", body, parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a list of events that match a set of conditions. The complete result is buffered into memory,
        /// so if a large result set is expected, use InSignalAsync() and lastReadEventId to page the results.
        /// </summary>
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
        /// <returns>The complete list of events, ordered from least to most recent.</returns>
        public async Task<ResultSetPart> InSignalAsync(
            SignalExpressionPart signal,
            string filter = null,
            int count = 30,
            string startAtId = null,
            string afterId = null,
            bool render = false,
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null,
            int? shortCircuitAfter = null,
            string permalinkId = null,
            CancellationToken cancellationToken = default)
        {
            if (signal == null) throw new ArgumentNullException(nameof(signal));

            var parameters = new Dictionary<string, object>
            {
                { "signal", signal.ToString() },
                { "count", count }
            };
            if (filter != null) { parameters.Add("filter", filter); }
            if (startAtId != null) { parameters.Add("startAtId", startAtId); }
            if (afterId != null) { parameters.Add("afterId", afterId); }
            if (render) { parameters.Add("render", true); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }
            if (shortCircuitAfter != null) { parameters.Add("shortCircuitAfter", shortCircuitAfter.Value); }
            if (permalinkId != null) { parameters.Add("permalinkId", permalinkId); }

            return await GroupGetAsync<ResultSetPart>("InSignal", parameters, cancellationToken).ConfigureAwait(false);
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
        /// <param name="cancellationToken">Token through which the operation can be cancelled.</param>
        /// <returns>A result carrying the count of events deleted.</returns>
        public async Task<DeleteResultPart> DeleteInSignalAsync(
            SignalEntity unsavedSignal = null,
            SignalExpressionPart signal = null,
            string filter = null,
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>();
            if (signal != null) { parameters.Add("signal", signal.ToString()); }
            if (filter != null) { parameters.Add("filter", filter); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }

            var body = unsavedSignal ?? new SignalEntity();
            return await GroupDeleteAsync<SignalEntity, DeleteResultPart>("DeleteInSignal", body, parameters, cancellationToken).ConfigureAwait(false);
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
