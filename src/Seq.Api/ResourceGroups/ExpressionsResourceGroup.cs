using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Expressions;

namespace Seq.Api.ResourceGroups
{
    public class ExpressionsResourceGroup : ApiResourceGroup
    {
        internal ExpressionsResourceGroup(ISeqConnection connection)
            : base("Expressions", connection)
        {
        }

        public Task<ExpressionPart> ToStrictAsync(string fuzzy, CancellationToken token = default)
        {
            return GroupGetAsync<ExpressionPart>("ToStrict", new Dictionary<string, object>
            {
                {"fuzzy", fuzzy}
            }, token);
        }

        public Task<ExpressionPart> ToSqlAsync(string fuzzy, CancellationToken token = default)
        {
            return GroupGetAsync<ExpressionPart>("ToSql", new Dictionary<string, object>
            {
                {"fuzzy", fuzzy}
            }, token);
        }
    }
}