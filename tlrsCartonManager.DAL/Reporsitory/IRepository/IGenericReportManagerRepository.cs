using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Models.Report;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IGenericReportManagerRepository
    {
        IList<IList<KeyValuePair<string, string>>> GetReportData(GenericReportData model);
        List<GenericReportColumn> GetReportColumns(string reportName);

        object GetReportDataforView(GenericReportData model);
    }
}
