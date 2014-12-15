using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Seq.Api.Model.Events;
using Seq.Api.Model.Queries;

namespace Seq.Api.ResourceGroups
{
    public class ReportsResourceGroup : ApiResourceGroup
    {
        internal ReportsResourceGroup(ISeqConnection connection)
            : base("Reports", connection)
        {
        }

        public async Task<ResultSetPart> RenderAsync(
            ReportFormat format,
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
            var parameters = new Dictionary<string, object>{{ "format", format.ToString().ToLowerInvariant() }};
            if (viewId != null) { parameters.Add("viewId", viewId); }
            if (count != null) { parameters.Add("count", count.Value); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }
            if (shortCircuitAfter != null) { parameters.Add("shortCircuitAfter", shortCircuitAfter.Value); }

            var body = query ?? new QueryEntity();
            return await GroupPostAsync<QueryEntity, ResultSetPart>("RenderQuery", body, parameters);
        }

        public async Task<ResultSetPart> RenderAsync(
            ReportFormat format,
            string queryId,
            string viewId = null,
            int? count = null,
            DateTime? fromDateUtc = null,
            DateTime? toDateUtc = null,
            int? shortCircuitAfter = null)
        {
            if (queryId == null) throw new ArgumentNullException("queryId");

            var parameters = new Dictionary<string, object>
            {
                {"queryId", queryId},
                {"format", format.ToString().ToLowerInvariant()}
            };
            if (viewId != null) { parameters.Add("viewId", viewId); }
            if (count != null) { parameters.Add("count", count.Value); }
            if (fromDateUtc != null) { parameters.Add("fromDateUtc", fromDateUtc.Value); }
            if (toDateUtc != null) { parameters.Add("toDateUtc", toDateUtc.Value); }
            if (shortCircuitAfter != null) { parameters.Add("shortCircuitAfter", shortCircuitAfter.Value); }

            return await GroupGetAsync<ResultSetPart>("RenderSavedQuery", parameters);
        }
    }
}
