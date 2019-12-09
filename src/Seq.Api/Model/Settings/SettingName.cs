// Copyright 2014-2019 Datalust and contributors. 
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Seq.Api.Model.Apps;
using Seq.Api.Model.Updates;
using Seq.Api.ResourceGroups;

namespace Seq.Api.Model.Settings
{
    /// <summary>
    /// Runtime-modifiable setting names. See also <see cref="SettingEntity"/> and
    /// <see cref="SettingsResourceGroup"/>.
    /// </summary>
    public enum SettingName
    {
        /// <summary>
        /// The name of an Active Directory group within which users will be automatically
        /// be granted user access to Seq.
        /// </summary>
        AutomaticAccessADGroup,

        /// <summary>
        /// If <c>true</c>, Azure Active Directory accounts in the configured tenant will
        /// be automatically granted user access to Seq.
        /// </summary>
        AutomaticallyGrantUserAccessToADAccounts,

        /// <summary>
        /// The Azure Active Directory client id.
        /// </summary>
        AzureADClientId,

        /// <summary>
        /// The Azure Active Directory client key.
        /// </summary>
        AzureADClientKey,

        /// <summary>
        /// The Azure Active Directory tenant id.
        /// </summary>
        AzureADTenantId,

        /// <summary>
        /// Server-local filesystem location where automatic backups are stored.
        /// </summary>
        BackupLocation,

        /// <summary>
        /// The number of backups to keep.
        /// </summary>
        BackupsToKeep,

        /// <summary>
        /// The UTC time of day to record new backups.
        /// </summary>
        BackupUtcTimeOfDay,

        /// <summary>
        /// If <c>true</c>, Seq will periodically check configured NuGet feed for
        /// updated versions of installed app packages, and
        /// set <see cref="AppPackagePart.UpdateAvailable"/> accordingly.
        /// </summary>
        CheckForPackageUpdates,

        /// <summary>
        /// If <c>true</c>, Seq will periodically check for new Seq versions, and
        /// create an <see cref="AvailableUpdateEntity"/> accordingly.
        /// </summary>
        CheckForUpdates,

        /// <summary>
        /// A friendly, public, human-readable title identifying this particular Seq instance.
        /// </summary>
        InstanceTitle,

        /// <summary>
        /// If <c>true</c>, the server supports Active Directory authentication.
        /// </summary>
        IsActiveDirectoryAuthentication,

        /// <summary>
        /// If <c>true</c>, the server has authentication enabled.
        /// </summary>
        IsAuthenticationEnabled,

        /// <summary>
        /// Obsolete.
        /// </summary>
        LazilyFlushEventWrites,

        /// <summary>
        /// Tracks whether an admin user has dismissed the master key backup warning.
        /// </summary>
        MasterKeyIsBackedUp,

        /// <summary>
        /// The minimum storage space, in bytes, on the disk containing log events, before
        /// Seq will stop accepting new events.
        /// </summary>
        MinimumFreeStorageSpace,

        /// <summary>
        /// A comma-separated list of (shared) signal ids that will be included in any new user's
        /// personal default workspace.
        /// </summary>
        NewUserShowSignalIds,

        /// <summary>
        /// A comma-separated list of (shared) SQL query ids that will be included in any new user's
        /// personal default workspace.
        /// </summary>
        NewUserShowQueryIds,

        /// <summary>
        /// A comma-separated list of (shared) dashboard ids that will be included in any new user's
        /// personal default workspace.
        /// </summary>
        NewUserShowDashboardIds,

        /// <summary>
        /// If <c>true</c>, ingestion requests incoming via HTTP must be authenticated using an API key or
        /// logged-in user session. Only effective when <see cref="IsAuthenticationEnabled"/> is <c>true</c>.
        /// </summary>
        RequireApiKeyForWritingEvents,

        /// <summary>
        /// The maximum size, in bytes of UTF-8-encoded JSON, beyond which individual events will be rejected.
        /// </summary>
        RawEventMaximumContentLength,

        /// <summary>
        /// The maximum size, in HTTP request content bytes, beyond which ingestion requests will be rejected.
        /// </summary>
        RawPayloadMaximumContentLength,

        /// <summary>
        /// A snippet of CSS that will be included in the front-end's user interface styles.
        /// </summary>
        ThemeStyles
    }
}
