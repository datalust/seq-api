using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Events;
using Seq.Api.Model.Queries;

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
            if (id == null) throw new ArgumentNullException("id");
            return await GroupGetAsync<EventEntity>("Item", new Dictionary<string, object> {{"id", id}});
        }

        public async Task<List<EventEntity>> ListAsync(
            string filter = null, 
            int? count = null,
            string start = null,
            string after = null, 
            bool render = false,
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null,
            int? shortCircuitAfter = null)
        {
            var parameters = new Dictionary<string, object>();
            if (filter != null) { parameters.Add("filter", filter); }
            if (count != null) { parameters.Add("count", count.Value); }
            if (start != null) { parameters.Add("start", start); }
            if (after != null) { parameters.Add("after", after); }
            if (render) { parameters.Add("render", true); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }
            if (shortCircuitAfter != null) { parameters.Add("shortCircuitAfter", shortCircuitAfter.Value); }

            return await GroupListAsync<EventEntity>("Items", parameters);
        }

        public async Task<ResultSetPart> MatchAsync(
            QueryEntity query = null,
            string viewId = null,
            string filter = null, 
            int? count = null,
            string start = null,
            string after = null, 
            bool render = false,
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null,
            int? shortCircuitAfter = null)
        {
            var parameters = new Dictionary<string, object>();
            if (viewId != null) { parameters.Add("viewId", viewId); }
            if (filter != null) { parameters.Add("filter", filter); }
            if (count != null) { parameters.Add("count", count.Value); }
            if (start != null) { parameters.Add("start", start); }
            if (after != null) { parameters.Add("after", after); }
            if (render) { parameters.Add("render", true); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }
            if (shortCircuitAfter != null) { parameters.Add("shortCircuitAfter", shortCircuitAfter.Value); }

            var body = query ?? new QueryEntity();
            return await GroupPostAsync<QueryEntity, ResultSetPart>("MatchQuery", body, parameters);
        }

        public async Task<ResultSetPart> MatchAsync(
            string queryId,
            string viewId = null,
            string filter = null, 
            int? count = null,
            string start = null,
            string after = null, 
            bool render = false,
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null,
            int? shortCircuitAfter = null)
        {
            if (queryId == null) throw new ArgumentNullException("queryId");

            var parameters = new Dictionary<string, object> {{"queryId", queryId}};
            if (viewId != null) { parameters.Add("viewId", viewId); }
            if (filter != null) { parameters.Add("filter", filter); }
            if (count != null) { parameters.Add("count", count.Value); }
            if (start != null) { parameters.Add("start", start); }
            if (after != null) { parameters.Add("after", after); }
            if (render) { parameters.Add("render", true); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }
            if (shortCircuitAfter != null) { parameters.Add("shortCircuitAfter", shortCircuitAfter.Value); }

            return await GroupGetAsync<ResultSetPart>("MatchSavedQuery", parameters);
        }

        public async Task<ResultSetPart> DeleteMatchedAsync(
            QueryEntity query = null,
            string viewId = null,
            string filter = null, 
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null)
        {
            var parameters = new Dictionary<string, object>();
            if (viewId != null) { parameters.Add("viewId", viewId); }
            if (filter != null) { parameters.Add("filter", filter); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }

            var body = query ?? new QueryEntity();
            return await GroupPostAsync<QueryEntity, ResultSetPart>("DeleteMatched", body, parameters);
        }
    }
}
