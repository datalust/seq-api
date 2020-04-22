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

namespace Seq.Api.Model.Security
{
    /// <summary>
    /// A permission is an access right 1) held by a principal, and 2) demanded by an endpoint. Permissions
    /// may be broad, such as the permission to modify administrative settings, or narrow (e.g. currently the
    /// permission to ingest events).
    /// </summary>
    public enum Permission
    {   
        /// <summary>
        /// A sentinel value to detect uninitialized permissions.
        /// </summary>
        Undefined,
        
        /// <summary>
        /// Access to publicly-visible assets - the API root/metadata, HTML, JavaScript, CSS, information necessary
        /// to initiate the login process, and so-on.
        /// </summary>
        Public,
        
        /// <summary>
        /// Add events to the event store.
        /// </summary>
        Ingest,
        
        /// <summary>
        /// Query events, dashboards, signals, app instances.
        /// </summary>
        Read,
        
        /// <summary>
        /// Write-access to signals, alerts, preferences etc.
        /// </summary>
        Write,
        
        /// <summary>
        /// Access to administrative features of Seq, management of other users, app installation, backups.
        /// </summary>
        Setup,
    }
}
