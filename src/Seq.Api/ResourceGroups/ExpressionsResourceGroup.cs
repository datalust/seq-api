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

        public Task<ExpressionPart> ToStrictAsync(string fuzzy, CancellationToken cancellationToken = default)
        {
            return GroupGetAsync<ExpressionPart>("ToStrict", new Dictionary<string, object>
            {
                {"fuzzy", fuzzy}
            }, cancellationToken);
        }

        public Task<ExpressionPart> ToSqlAsync(string fuzzy, CancellationToken cancellationToken = default)
        {
            return GroupGetAsync<ExpressionPart>("ToSql", new Dictionary<string, object>
            {
                {"fuzzy", fuzzy}
            }, cancellationToken);
        }
    }
}