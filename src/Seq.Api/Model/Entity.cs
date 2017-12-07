namespace Seq.Api.Model
{
    public abstract class Entity : ILinked
    {
        protected Entity()
        {
            Links = new LinkCollection();
        }

        public string Id { get; set; }

        public LinkCollection Links { get; set; }
    }
}
