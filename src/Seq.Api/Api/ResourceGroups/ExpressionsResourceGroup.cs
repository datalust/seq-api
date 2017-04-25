using System.Collections.Generic;
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

        public Task<ExpressionPart> ToStrictAsync(string fuzzy)
        {
            return GroupGetAsync<ExpressionPart>("ToStrict", new Dictionary<string, object>
            {
                {"fuzzy", fuzzy}
            });
        }

        public Task<ExpressionPart> ToSqlAsync(string fuzzy)
        {
            return GroupGetAsync<ExpressionPart>("ToSql", new Dictionary<string, object>
            {
                {"fuzzy", fuzzy}
            });
        }
    }
}