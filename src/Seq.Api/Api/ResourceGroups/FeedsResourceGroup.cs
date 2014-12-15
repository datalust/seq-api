namespace Seq.Api.ResourceGroups
{
    public class FeedsResourceGroup : ApiResourceGroup
    {
        internal FeedsResourceGroup(ISeqConnection connection)
            : base("Feeds", connection)
        {
        }
    }
}