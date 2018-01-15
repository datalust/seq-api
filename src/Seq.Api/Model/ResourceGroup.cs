namespace Seq.Api.Model
{
    public class ResourceGroup : ILinked
    {
        public ResourceGroup()
        {
            Links = new LinkCollection();
        }

        public LinkCollection Links { get; set; }
    }
}
