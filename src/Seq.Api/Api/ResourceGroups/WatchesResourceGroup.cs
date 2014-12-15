namespace Seq.Api.ResourceGroups
{
    public class WatchesResourceGroup : ApiResourceGroup
    {
        internal WatchesResourceGroup(ISeqConnection connection)
            : base("Watches", connection)
        {
        }
    }
}