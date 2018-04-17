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
