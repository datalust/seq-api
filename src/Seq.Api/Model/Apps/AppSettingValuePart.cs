namespace Seq.Api.Model.Apps
{
    /// <summary>
    /// Describes a value accepted for an <see cref="AppSettingPart"/> with type <c>Select</c>.
    /// </summary>
    public class AppSettingValuePart
    {
        /// <summary>
        /// The value accepted for the setting.
        /// </summary>
        public string Value { get; set; }
        
        /// <summary>
        /// Optionally, a description of the value, which Seq will use as the value's label in the UI. By default,
        /// the <see cref="Value"/> will be used as the label.
        /// </summary>
        public string Description { get; set; }
    }
}