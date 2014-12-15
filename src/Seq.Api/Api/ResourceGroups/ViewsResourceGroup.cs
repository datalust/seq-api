namespace Seq.Api.ResourceGroups
{
    public class ViewsResourceGroup : ApiResourceGroup
    {
        internal ViewsResourceGroup(ISeqConnection connection)
            : base("Views", connection)
        {
        }
    }
}