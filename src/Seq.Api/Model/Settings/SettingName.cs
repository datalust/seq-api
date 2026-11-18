// Copyright © Datalust and contributors. 
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

using System;
using Seq.Api.Model.Apps;
using Seq.Api.Model.Updates;
using Seq.Api.Model.Users;
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
        /// The authentication provider to use. Allowed values are <c>null</c> (local username/password),
        /// <c>"Active Directory"</c>, <c>"Microsoft Entra ID"</c> and <c>"OpenID Connect"</c>.
        /// </summary>
        AuthenticationProvider,
        
        /// <summary>
        /// The name of an Active Directory group within which users will be automatically
        /// be granted user access to Seq.
        /// </summary>
        AutomaticAccessADGroup,

        /// <summary>
        /// If <c>true</c>, Microsoft Entra ID accounts in the configured tenant will
        /// be automatically granted user access to Seq.
        /// </summary>
        [Obsolete("Use `AutomaticallyProvisionAuthenticatedUsers`.", error: true)]
        AutomaticallyGrantUserAccessToADAccounts,
        
        /// <summary>
        /// If <c>true</c>, users authenticated with the configured authentication provider
        /// be automatically granted default user access to Seq.
        /// </summary>
        AutomaticallyProvisionAuthenticatedUsers,
        
        /// <summary>
        /// In a clustered configuration, the maximum data age allowed before a warning notification is generated
        /// for an out-of-date follower node.
        /// </summary>
        DataAgeWarningThresholdMilliseconds,

        /// <summary>
        /// The Microsoft Entra ID authority. The default is <c>login.microsoftonline.com</c>; government cloud users may
        /// require <c>login.microsoftonline.us</c> or similar.
        /// </summary>
        EntraIdAuthority,
        
        /// <summary>
        /// The Microsoft Entra ID client id.
        /// </summary>
        EntraIdClientId,

        /// <summary>
        /// The Microsoft Entra ID client key.
        /// </summary>
        EntraIdClientKey,

        /// <summary>
        /// The Microsoft Entra ID tenant id.
        /// </summary>
        EntraIdTenantId,
        
        /// <summary>
        /// The Microsoft Entra ID credential source type. By default (<c>ClientSecret</c>), a client key (client
        /// secret) is expected in <see cref="EntraIdClientKey"/>. Specify <c>SignedAssertionFromManagedIdentity</c>
        /// and, optionally, a value for <see cref="EntraIdManagedIdentityClientId"/> to authenticate using a
        /// managed identity instead. If no <see cref="EntraIdManagedIdentityClientId"/> is specified, the
        /// system-assigned managed identity is used. Specify <c>SignedAssertionFile</c> to authenticate using
        /// a signed assertion (for example, a Kubernetes projected service account token) read from the file named
        /// by the <c>AZURE_FEDERATED_TOKEN_FILE</c> environment variable; the file is re-read on each sign-in so
        /// that rotated tokens are picked up. Specify <c>CertificateFile</c> to authenticate using a client
        /// certificate and private key from the Seq server's certificates folder.
        /// Specify <c>CertificateThumbprint</c> or <c>CertificateSubject</c> to authenticate using a client
        /// certificate identified by <see cref="EntraIdClientCertificateThumbprint"/> or
        /// <see cref="EntraIdClientCertificateSubject"/>, respectively; these options are only supported on Windows.
        /// </summary>
        EntraIdCredentialType,
        
        /// <summary>
        /// The client id of the user-assigned managed identity that the app registration's federated identity
        /// credential trusts. Considered only when <see cref="EntraIdCredentialType"/> is
        /// <c>SignedAssertionFromManagedIdentity</c>.
        /// </summary>
        EntraIdManagedIdentityClientId,

        /// <summary>
        /// The SHA-1 thumbprint of a client certificate, with private key, in the <c>LocalMachine</c> or
        /// <c>CurrentUser</c> Personal (<c>My</c>) Windows certificate store. Considered only when
        /// <see cref="EntraIdCredentialType"/> is <c>CertificateThumbprint</c>.
        /// </summary>
        EntraIdClientCertificateThumbprint,

        /// <summary>
        /// The subject common name, or full distinguished name, of a client certificate in the
        /// <c>LocalMachine</c> or <c>CurrentUser</c> Personal (<c>My</c>) Windows certificate store. The newest
        /// currently-valid matching certificate with a private key is used, so that rolled-over certificates are
        /// picked up automatically. Considered only when <see cref="EntraIdCredentialType"/> is
        /// <c>CertificateSubject</c>.
        /// </summary>
        EntraIdClientCertificateSubject,

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
        [Obsolete("Set `AuthenticationProvider` to \"Active Directory\" to enable.", error: true)]
        IsActiveDirectoryAuthentication,

        /// <summary>
        /// If <c>true</c>, the server has authentication enabled.
        /// </summary>
        IsAuthenticationEnabled,

        /// <summary>
        /// The minimum storage space, in bytes, on the disk containing log events, before
        /// Seq will stop accepting new events.
        /// </summary>
        MinimumFreeStorageSpace,

        /// <summary>
        /// The minimum length of passwords for local username/password authentication.
        /// </summary>
        MinimumPasswordLength,
        
        /// <summary>
        /// A dictionary of `(string, string)` pairs that will be used to initialize the
        /// <see cref="UserEntity.Preferences"/> property when preparing new user entities
        /// with <see cref="UsersResourceGroup.TemplateAsync"/>, and with automatically
        /// provisioning SSO users (when enabled).
        /// </summary>
        /// <remarks>User preference keys are unconstrained; the Seq UI uses a number of these, but
        /// alternative interfaces and integrations may add additional items to this collection.</remarks>
        NewUserPreferences,

        /// <summary>
        /// A comma-separated list of role ids that will be assigned to new users by default.
        /// </summary>
        NewUserRoleIds,

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
        /// A comma-separated list of (shared) metrics view ids that will be included in any new user's
        /// personal default workspace.
        /// </summary>
        NewUserShowViewIds,

        /// <summary>
        /// If using OpenID Connect authentication, the URL of the authorization endpoint. For example,
        /// <c>https://example.com</c>.
        /// </summary>
        OpenIdConnectAuthority,
        
        /// <summary>
        /// If using OpenID Connect, the client id assigned to Seq in the provider.
        /// </summary>
        OpenIdConnectClientId,
        
        /// <summary>
        /// If using OpenID Connect, the client secret assigned to Seq in the provider.
        /// </summary>
        OpenIdConnectClientSecret,

        /// <summary>
        /// If using OpenID Connect, the scopes Seq will request when authorizing the client, as a comma-separated
        /// list. For example, <c>openid, profile, email</c>.
        /// </summary>
        OpenIdConnectScopes,

        /// <summary>
        /// If using OpenID Connect, the URI that the provider will redirect to after the user signs out.
        /// </summary>
        OpenIdConnectEndSessionRedirectUri,
        
        /// <summary>
        /// If using OpenID Connect, overrides the URI of the provider's metadata endpoint.
        /// </summary>
        OpenIdConnectMetadataAddress,

        /// <summary>
        /// The character classes that local username/password authentication passwords must include, as a string
        /// containing any of <c>a</c> (lowercase letter), <c>A</c> (uppercase letter), <c>0</c> (digit), and
        /// <c>!</c> (punctuation).
        /// </summary>
        PasswordRequiredCharacterClasses,
    
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
        /// If <c>true</c>, the SCIM 2.0 user provisioning endpoints under <c>/scim/v2</c> are enabled. Only
        /// effective when <see cref="IsAuthenticationEnabled"/> is <c>true</c>.
        /// </summary>
        ScimEnabled,

        /// <summary>
        /// The default duration, in seconds, that searches will cover.
        /// </summary>
        SearchDurationSeconds,

        /// <summary>
        /// An expression that identifies the service associated with an event or span.
        /// </summary>
        ServiceNameExpression,

        /// <summary>
        /// Tracks whether an admin user has dismissed the secret key backup warning.
        /// </summary>
        SecretKeyIsBackedUp,
        
        /// <summary>
        /// In a clustered configuration, the minimum number of nodes that must carry up-to-date data in order for
        /// operations like graceful fail-over to proceed.
        /// </summary>
        TargetReplicaCount,
        
        /// <summary>
        /// A snippet of CSS that will be included in the front-end's user interface styles.
        /// </summary>
        ThemeStyles,

        /// <summary>
        /// If <c>true</c>, user avatars will be retrieved from Gravatar.
        /// </summary>
        UseGravatar
    }
}
