namespace Seq.Api.ResourceGroups
{
    public class SettingsResourceGroup : ApiResourceGroup
    {
        internal SettingsResourceGroup(ISeqConnection connection)
            : base("Settings", connection)
        {
        }
    }
}