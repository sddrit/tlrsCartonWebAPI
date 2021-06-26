using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Dtos.MetaData;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.MetaData;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IDocketPrintManagerRepository
    {
        object GetDocket(DocketPrintModel model);
        object GetDocketRePrint(DocketRePrintModel model);
        List<DocketPrintResultModel> GetBulkDocket(DocketPrintBulkModel model);
        bool DeleteDocketRePrint(DocketRePrintModel model);
        PagedResponse<ViewPrintedDocket> SearchDockets(string printStatus, string searchText, int pageIndex, int pageSize);
        List<T> GetCartonsToDocket<T>(DocketPrintModel model, out int serialNo) where T : class;


    }
}
