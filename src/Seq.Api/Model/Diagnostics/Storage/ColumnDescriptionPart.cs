using System;

namespace Seq.Api.Model.Diagnostics.Storage
{
    /// <summary>
    /// A description of a column in a rowset.
    /// </summary>
    public readonly struct ColumnDescriptionPart
    {
        /// <summary>
        /// A label for the column.
        /// </summary>
        public string Label { get; }
        
        /// <summary>
        /// Additional metadata describing the role of the column; this is separate from,
        /// but related to, the runtime type of the column values.
        /// </summary>
        public ColumnType Type { get; }

        /// <summary>
        /// Construct a <see cref="ColumnDescriptionPart"/>.
        /// </summary>
        /// <param name="label">A label for the column.</param>
        /// <param name="type">Additional metadata describing the role of the column; this is separate from,
        /// but related to, the runtime type of the column values.</param>
        public ColumnDescriptionPart(string label, ColumnType type)
        {
            Label = label ?? throw new ArgumentNullException(nameof(label));
            Type = type;
        }
    }
}