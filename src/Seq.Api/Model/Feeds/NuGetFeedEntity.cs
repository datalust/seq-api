namespace Seq.Api.Model.Feeds
{
    public class NuGetFeedEntity : Seq.Api.Model.Entity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Username { get; set; }
        public string NewPassword { get; set; }
    }
}
