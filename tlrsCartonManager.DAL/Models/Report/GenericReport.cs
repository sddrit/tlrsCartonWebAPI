using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Report
{
    public class GenericReportColumn
    {
        public int ColumnId { get; set; }
        public string  ColumnName { get; set; }
        public ICollection<GenericReportFilterOption> FilterOptionList { get; set; }
    }
    public class GenericReportFilterOption
    {
        public string Filter{ get; set; }
        public string FromValue { get; set; }
        public string ToValue { get; set; }
        public string Select { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }


    }
}
