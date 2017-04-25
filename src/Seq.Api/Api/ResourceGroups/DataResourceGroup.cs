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

        public async Task<QueryResultPart> QueryAsync(
            string query,
            DateTime rangeStartUtc,
            DateTime? rangeEndUtc = null,
            SignalEntity signal = null,
            string[] intersectIds = null)
        {
            SignalEntity body;
            Dictionary<string, object> parameters;
            MakeParameters(query, rangeStartUtc, rangeEndUtc, signal, intersectIds, out body, out parameters);
            return await GroupPostAsync<SignalEntity, QueryResultPart>("Query", body, parameters).ConfigureAwait(false);
        }

        public async Task<string> QueryCsvAsync(
            string query,
            DateTime rangeStartUtc,
            DateTime? rangeEndUtc = null,
            SignalEntity signal = null,
            string[] intersectIds = null)
        {
            SignalEntity body;
            Dictionary<string, object> parameters;
            MakeParameters(query, rangeStartUtc, rangeEndUtc, signal, intersectIds, out body, out parameters);
            parameters.Add("format", "text/csv");
            return await GroupPostReadStringAsync("Query", body, parameters).ConfigureAwait(false);
        }

        static void MakeParameters(string query, DateTime rangeStartUtc, DateTime? rangeEndUtc, SignalEntity signal,
            string[] intersectIds, out SignalEntity body, out Dictionary<string, object> parameters)
        {
            parameters = new Dictionary<string, object>
            {
                {"q", query},
                {nameof(rangeStartUtc), rangeStartUtc}
            };

            if (rangeEndUtc != null)
            {
                parameters.Add(nameof(rangeEndUtc), rangeEndUtc.Value);
            }
            if (intersectIds != null && intersectIds.Length > 0)
            {
                parameters.Add(nameof(intersectIds), string.Join(",", intersectIds));
            }

            body = signal ?? new SignalEntity();
        }
    }
}
