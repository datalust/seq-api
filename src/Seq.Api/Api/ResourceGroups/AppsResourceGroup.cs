namespace Seq.Api.ResourceGroups
{
    public class AppsResourceGroup : ApiResourceGroup
    {
        internal AppsResourceGroup(ISeqConnection connection)
            : base("Apps", connection)
        {
        }
    }
}