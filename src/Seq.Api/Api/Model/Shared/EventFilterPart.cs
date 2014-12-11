namespace Seq.Api.Model.Shared
{
    public class EventFilterPart
    {
        public string Description { get; set; }
        public string Filter { get; set; }
        public bool DisplayAsExclusion { get; set; }
        public bool IsFriendlyDescription { get; set; }
    }
}
