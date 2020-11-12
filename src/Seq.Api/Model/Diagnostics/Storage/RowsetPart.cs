namespace Seq.Api.Model.Diagnostics.Storage
{
    /// <summary>
    /// Values in rows and columns.
    /// </summary>
    public class RowsetPart
    {
        /// <summary>
        /// The columns of the rowset.
        /// </summary>
        public ColumnDescriptionPart[] Columns { get; set; }
        
        /// <summary>
        /// An array of rows, where each row is an array of values
        /// corresponding to the columns of the rowset.
        /// </summary>
        public object[][] Rows { get; set; }
    }
}