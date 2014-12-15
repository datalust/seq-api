namespace Seq.Api.ResourceGroups
{
    public class ReportsResourceGroup : ApiResourceGroup
    {
        internal ReportsResourceGroup(ISeqConnection connection)
            : base("Reports", connection)
        {
        }
    }
}