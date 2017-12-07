namespace Seq.Api.Model.Monitoring
{
    public class MeasurementDisplayStylePart
    {
        public MeasurementDisplayType Type { get; set; } = MeasurementDisplayType.Line;
        public bool LineFillToZeroY { get; set; }
        public bool LineShowMarkers { get; set; } = true;
        public bool BarOverlaySum { get; set; }
        public MeasurementDisplayPalette Palette { get; set; } = MeasurementDisplayPalette.Default;
    }
}
