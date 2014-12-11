namespace Seq.Api.Model.Settings
{
    public class SettingEntity : Seq.Api.Model.Entity
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
