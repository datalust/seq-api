using System.Collections.Generic;

namespace Seq.Api.Model.Watches
{
    public class DataRangePart
    {
        public DataRangePart()
        {
            Points = new List<DataPointPart>();
        }

        public IList<DataPointPart> Points { get; set; } 
    }
}
