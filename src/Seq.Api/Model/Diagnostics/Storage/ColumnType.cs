namespace Seq.Api.Model.Diagnostics.Storage
{
    /// <summary>
    /// Additional metadata describing the role of a column; this is separate from,
    /// but related to, the runtime type of the column values.
    /// </summary>
    public enum ColumnType
    {
        /// <summary>
        /// The column contains general data.
        /// </summary>
        General,
        
        /// <summary>
        /// The column contains timestamps that may be used to create a timeseries.
        /// </summary>
        Timestamp,
    }
}