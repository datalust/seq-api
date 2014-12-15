namespace Seq.Api.ResourceGroups
{
    public class ApiKeysResourceGroup : ApiResourceGroup
    {
        internal ApiKeysResourceGroup(ISeqConnection connection)
            : base("ApiKeys", connection)
        {
        }
    }
}