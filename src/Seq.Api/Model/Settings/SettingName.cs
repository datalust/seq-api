namespace Seq.Api.Model.Settings
{
    public enum SettingName
    {
        AutomaticAccessADGroup,
        AutomaticallyGrantUserAccessToADAccounts,
        AzureADClientId,
        AzureADClientKey,
        AzureADTenantId,
        BackupLocation,
        BackupsToKeep,
        BackupUtcTimeOfDay,
        CheckForPackageUpdates,
        CheckForUpdates,
        InstanceTitle,
        IsActiveDirectoryAuthentication,
        IsAuthenticationEnabled,
        LazilyFlushEventWrites,
        MasterKeyIsBackedUp,
        MinimumFreeStorageSpace,
        RequireApiKeyForWritingEvents,
        RawEventMaximumContentLength,
        RawPayloadMaximumContentLength,
        ThemeStyles
    }
}