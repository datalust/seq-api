using System;

namespace Seq.Api.Model.Shared
{
    /// <summary>
    /// A range represented by a start and end <see cref="DateTime"/>.
    /// </summary>
    public readonly struct DateTimeRangePart
    {
        /// <summary>
        /// The (inclusive) start of the range.
        /// </summary>
        public DateTime Start { get; }
        
        /// <summary>
        /// The (exclusive) end of the range.
        /// </summary>
        public DateTime End { get; }

        /// <summary>
        /// Construct a <see cref="DateTimeRangePart"/>.
        /// </summary>
        /// <param name="start">The (inclusive) start of the range.</param>
        /// <param name="end">The (exclusive) end of the range.</param>
        public DateTimeRangePart(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}