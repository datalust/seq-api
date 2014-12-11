namespace Seq.Api.Model.License
{
    public class LicenseEntity : Seq.Api.Model.Entity
    {
        public string LicenseText { get; set; }
        public bool IsValid { get; set; }
        public bool IsSingleUser { get; set; }
        public string StatusDescription { get; set; }
        public bool IsWarning { get; set; }
    }
}
