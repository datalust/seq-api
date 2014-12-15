namespace Seq.Api.ResourceGroups
{
    public class RetentionPoliciesResourceGroup : ApiResourceGroup
    {
        internal RetentionPoliciesResourceGroup(ISeqConnection connection)
            : base("RetentionPolicies", connection)
        {
        }
    }
}