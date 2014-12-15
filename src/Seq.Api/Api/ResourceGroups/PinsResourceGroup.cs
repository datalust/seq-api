namespace Seq.Api.ResourceGroups
{
    public class PinsResourceGroup : ApiResourceGroup
    {
        internal PinsResourceGroup(ISeqConnection connection)
            : base("Pins", connection)
        {
        }
    }
}