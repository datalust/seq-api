using System.Collections.Generic;

namespace Seq.Api.Model.Reports
{
    public class ReportResultPart
    {
        public List<string> Columns { get; set; }
        public List<ReportRowPart> Rows { get; set; }
    }
}
