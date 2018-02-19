using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Data;
using Seq.Api.Model.Signals;

namespace Seq.Api.ResourceGroups
{
    public class DataResourceGroup : ApiResourceGroup
    {
        internal DataResourceGroup(ISeqConnection connection)
            : base("Data", connection)
        {
        }

        /// <summary>
        /// Execute an SQL query and retrieve the result set as a structured <see cref="QueryResultPart"/>.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <param name="rangeStartUtc">The earliest timestamp from which to include events in the query result.</param>
        /// <param name="rangeEndUtc">The exclusive latest timestamp to which events are included in the query result. The default is the current time.</param>
        /// <param name="signal">A signal expression over which the query will be executed.</param>
        /// <param name="unsavedSignal">A constructed signal that may not appear on the server, for example, a <see cref="SignalEntity"/> that has been
        /// created but not saved, a signal from another server, or the modified representation of an entity already persisted.</param>
        /// <param name="timeout">The query timeout; if not specified, the query will run until completion.</param>
        /// <returns>A structured result set.</returns>
        public async Task<QueryResultPart> QueryAsync(
            string query,
            DateTime? rangeStartUtc = null,
            DateTime? rangeEndUtc = null,
            SignalExpressionPart signal = null,
            SignalEntity unsavedSignal = null,
            TimeSpan? timeout = null)
        {
            MakeParameters(query, rangeStartUtc, rangeEndUtc, signal, unsavedSignal, timeout, out var body, out var parameters);
            return await GroupPostAsync<SignalEntity, QueryResultPart>("Query", body, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute an SQL query and retrieve the result set as a structured <see cref="QueryResultPart"/>.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <param name="rangeStartUtc">The earliest timestamp from which to include events in the query result.</param>
        /// <param name="rangeEndUtc">The exclusive latest timestamp to which events are included in the query result. The default is the current time.</param>
        /// <param name="signal">A signal expression over which the query will be executed.</param>
        /// <param name="unsavedSignal">A constructed signal that may not appear on the server, for example, a <see cref="SignalEntity"/> that has been
        /// created but not saved, a signal from another server, or the modified representation of an entity already persisted.</param>
        /// <param name="timeout">The query timeout; if not specified, the query will run until completion.</param>
        /// <returns>A CSV result set.</returns>
        public async Task<string> QueryCsvAsync(
            string query,
            DateTime? rangeStartUtc = null,
            DateTime? rangeEndUtc = null,
            SignalExpressionPart signal = null,
            SignalEntity unsavedSignal = null,
            TimeSpan? timeout = null)
        {
            MakeParameters(query, rangeStartUtc, rangeEndUtc, signal, unsavedSignal, timeout, out var body, out var parameters);
            parameters.Add("format", "text/csv");
            return await GroupPostReadStringAsync("Query", body, parameters).ConfigureAwait(false);
        }

        static void MakeParameters(
            string query,
            DateTime? rangeStartUtc,
            DateTime? rangeEndUtc,
            SignalExpressionPart signal,
            SignalEntity unsavedSignal,
            TimeSpan? timeout,
            out SignalEntity body,
            out Dictionary<string, object> parameters)
        {
            parameters = new Dictionary<string, object>
            {
                {"q", query}
            };

            if (rangeStartUtc != null)
            {
                parameters.Add(nameof(rangeStartUtc), rangeStartUtc);
            }
            if (rangeEndUtc != null)
            {
                parameters.Add(nameof(rangeEndUtc), rangeEndUtc.Value);
            }
            if (signal != null)
            {
                parameters.Add(nameof(signal), signal.ToString());
            }
            if (timeout != null)
            {
                parameters.Add("timeoutMS", timeout.Value.TotalMilliseconds.ToString("0"));
            }

            body = unsavedSignal ?? new SignalEntity();
        }
    }
}
