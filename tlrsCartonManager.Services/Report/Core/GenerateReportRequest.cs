using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.Services.Report.Core
{
    public class GenerateReportRequest
    {
        public string ReportName { get; set; }
        public ICollection<GenerateReportOption> ReportFilters { get; set; }

    }
    public class GenerateReportOption
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public string FilterOperator { get; set; }
        public string FromValue { get; set; }
        public string ToValue { get; set; }
        public bool IsSelect { get; set; }
        public string SortBy { get; set; }
        public int SortOrder { get; set; }
    }
}
