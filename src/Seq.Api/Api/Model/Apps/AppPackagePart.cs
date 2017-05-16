namespace Seq.Api.Model.Apps
{
    public class AppPackagePart
    {
        public string NuGetFeedId { get; set; }
        public string PackageId { get; set; }
        public string Version { get; set; }
        public string Authors { get; set; }
        public string IconUrl { get; set; }
        public string LicenseUrl { get; set; }
        public bool UpdateAvailable { get; set; }
    }
}
