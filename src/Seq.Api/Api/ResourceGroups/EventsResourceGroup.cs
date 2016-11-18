using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<EventEntity> FindAsync(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<EventEntity>("Item", new Dictionary<string, object> { { "id", id } }).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a list of events that match a set of conditions. The complete result is buffered into memory,
        /// so if a large result set is expected, use InSignalAsync() and lastReadEventId to page the results.
        /// </summary>
        /// <param name="intersectIds">If provided, a list of signal ids whose intersection will be filtered for the result.</param>
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
        /// <returns>The complete list of events, ordered from least to most recent.</returns>
        public async Task<List<EventEntity>> ListAsync(
            string[] intersectIds = null,
            string filter = null, 
            int count = 30,
            string startAtId = null,
            string afterId = null,
            bool render = false,
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null,
            int? shortCircuitAfter = null)
        {
            var parameters = new Dictionary<string, object> { { "count", count } };
            if (intersectIds != null && intersectIds.Length > 0) { parameters.Add("intersectIds", string.Join(",", intersectIds)); }
            if (filter != null) { parameters.Add("filter", filter); }
            if (startAtId != null) { parameters.Add("startAtId", startAtId); }
            if (afterId != null) { parameters.Add("afterId", afterId); }
            if (render) { parameters.Add("render", true); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }
            if (shortCircuitAfter != null) { parameters.Add("shortCircuitAfter", shortCircuitAfter.Value); }

            var chunks = new List<List<EventEntity>>();
            var remaining = count;

            while (true)
            {
                var resultSet = await GroupGetAsync<ResultSetPart>("InSignal", parameters).ConfigureAwait(false);
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

        public async Task<ResultSetPart> InSignalAsync(
            SignalEntity signal = null,
            string[] intersectIds = null,
            string filter = null, 
            int count = 30,
            string startAtId = null,
            string afterId = null, 
            bool render = false,
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null,
            int? shortCircuitAfter = null)
        {
            var parameters = new Dictionary<string, object>{{ "count", count }};
            if (intersectIds != null && intersectIds.Length > 0) { parameters.Add("intersectIds", string.Join(",", intersectIds)); }
            if (filter != null) { parameters.Add("filter", filter); }
            if (startAtId != null) { parameters.Add("startAtId", startAtId); }
            if (afterId != null) { parameters.Add("afterId", afterId); }
            if (render) { parameters.Add("render", true); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }
            if (shortCircuitAfter != null) { parameters.Add("shortCircuitAfter", shortCircuitAfter.Value); }

            var body = signal ?? new SignalEntity();
            return await GroupPostAsync<SignalEntity, ResultSetPart>("InSignal", body, parameters).ConfigureAwait(false);
        }

        public async Task<ResultSetPart> InSignalAsync(
            string[] intersectIds,
            string filter = null, 
            int count = 30,
            string startAtId = null,
            string afterId = null, 
            bool render = false,
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null,
            int? shortCircuitAfter = null)
        {
            if (intersectIds == null) throw new ArgumentNullException(nameof(intersectIds));

            var parameters = new Dictionary<string, object>
            {
                { "intersectIds", string.Join(",", intersectIds) },
                { "count", count }
            };
            if (filter != null) { parameters.Add("filter", filter); }
            if (startAtId != null) { parameters.Add("startAtId", startAtId); }
            if (afterId != null) { parameters.Add("afterId", afterId); }
            if (render) { parameters.Add("render", true); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }
            if (shortCircuitAfter != null) { parameters.Add("shortCircuitAfter", shortCircuitAfter.Value); }

            return await GroupGetAsync<ResultSetPart>("InSignal", parameters).ConfigureAwait(false);
        }

        public async Task<ResultSetPart> DeleteInSignalAsync(
            SignalEntity signal = null,
            string[] intersectIds = null,
            string filter = null, 
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null)
        {
            var parameters = new Dictionary<string, object>();
            if (intersectIds != null && intersectIds.Length > 0) { parameters.Add("intersectIds", string.Join(",", intersectIds)); }
            if (filter != null) { parameters.Add("filter", filter); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }

            var body = signal ?? new SignalEntity();
            return await GroupPostAsync<SignalEntity, ResultSetPart>("DeleteInSignal", body, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Connect to the live event stream. Dispose the resulting stream to disconnect.
        /// </summary>
        /// <typeparam name="T">The type into which events should be deserialized.</typeparam>
        /// <param name="intersectIds">If provided, a list of signal ids whose intersection will be filtered for the result.</param>
        /// <param name="filter">A strict Seq filter expression to match (text expressions must be in double quotes). To
        /// convert a "fuzzy" filter into a strict one the way the Seq UI does, use connection.Expressions.ToStrictAsync().</param>
        /// <returns>An observable that will stream events from the server to subscribers. Events will be buffered server-side until the first
        /// subscriber connects, ensure at least one subscription is made in order to avoid event loss.</returns>
        public async Task<ObservableStream<T>> StreamAsync<T>(
            string[] intersectIds = null,
            string filter = null)
        {
            var parameters = new Dictionary<string, object>();
            if (intersectIds != null && intersectIds.Length > 0) { parameters.Add("intersectIds", string.Join(",", intersectIds)); }
            if (filter != null) { parameters.Add("filter", filter); }

            var group = await LoadGroupAsync().ConfigureAwait(false);
            return await Client.StreamAsync<T>(group, "Stream", parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a list of events that match a set of conditions. The complete result is buffered into memory,
        /// so if a large result set is expected, use InSignalAsync() and lastReadEventId to page the results.
        /// </summary>
        /// <param name="intersectIds">If provided, a list of signal ids whose intersection will be filtered for the result.</param>
        /// <param name="filter">A strict Seq filter expression to match (text expressions must be in double quotes). To
        /// convert a "fuzzy" filter into a strict one the way the Seq UI does, use connection.Expressions.ToStrictAsync().</param>
        /// <returns>An observable that will stream events from the server to subscribers. Events will be buffered server-side until the first
        /// subscriber connects, ensure at least one subscription is made in order to avoid event loss.</returns>
        public async Task<ObservableStream<string>> StreamDocumentsAsync(
            string[] intersectIds = null,
            string filter = null)
        {
            var parameters = new Dictionary<string, object>();
            if (intersectIds != null && intersectIds.Length > 0) { parameters.Add("intersectIds", string.Join(",", intersectIds)); }
            if (filter != null) { parameters.Add("filter", filter); }

            var group = await LoadGroupAsync().ConfigureAwait(false);
            return await Client.StreamTextAsync(group, "Stream", parameters).ConfigureAwait(false);
        }
    }
}
