namespace Seq.Api.ResourceGroups
{
    public class QueriesResourceGroup : ApiResourceGroup
    {
        internal QueriesResourceGroup(ISeqConnection connection)
            : base("Queries", connection)
        {
        }
    }
}