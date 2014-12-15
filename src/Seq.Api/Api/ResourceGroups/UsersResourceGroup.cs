namespace Seq.Api.ResourceGroups
{
    public class UsersResourceGroup : ApiResourceGroup
    {
        internal UsersResourceGroup(ISeqConnection connection)
            : base("Users", connection)
        {
        }
    }
}