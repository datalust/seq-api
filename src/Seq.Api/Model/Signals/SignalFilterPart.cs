namespace Seq.Api.Model.Signals
{
    public class SignalFilterPart
    {
        public string Description { get; set; }
        public bool DescriptionIsExcluded { get; set; }
        public string Filter { get; set; }
        public string FilterNonStrict { get; set; }
    }
}
