namespace Seq.Api.Model.Apps
{
    public class AppSettingPart
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsOptional { get; set; }
        public string HelpText { get; set; }
        public string Type { get; set; }
    }
}
