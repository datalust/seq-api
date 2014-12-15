namespace Seq.Api.ResourceGroups
{
    public class LicensesResourceGroup : ApiResourceGroup
    {
        internal LicensesResourceGroup(ISeqConnection connection)
            : base("Licenses", connection)
        {
        }
    }
}