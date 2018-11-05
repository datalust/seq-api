namespace Seq.Api.Model.Workspaces
{
    public class WorkspaceEntity : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public bool IsProtected { get; set; }

        public WorkspaceContentPart Content { get; set; } = new WorkspaceContentPart();
    }
}
