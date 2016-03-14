namespace Seq.Api.Data
{
    public class TimeseriesPart
    {
        public object[] Key { get; set; }
        public TimeSlicePart[] Slices { get; set; }
    }
}