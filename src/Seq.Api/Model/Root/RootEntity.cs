namespace Seq.Api.Model.Root
{
    public class RootEntity : ILinked
    {
        public RootEntity()
        {
            Links = new LinkCollection();
        }

        public string Product { get; set; }
        public string Version { get; set; }
        public string InstanceName { get; set; }
        public LinkCollection Links { get; set; }
    }
}
