namespace Seq.Api.ResourceGroups
{
    public class MetricsResourceGroup : ApiResourceGroup
    {
        internal MetricsResourceGroup(ISeqConnection connection)
            : base("Metrics", connection)
        {
        }
    }
}