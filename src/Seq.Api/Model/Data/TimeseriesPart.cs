namespace Seq.Api.Model.Data
{
    public class TimeseriesPart
    {
        public object[] Key { get; set; }
        public TimeSlicePart[] Slices { get; set; }
    }
}