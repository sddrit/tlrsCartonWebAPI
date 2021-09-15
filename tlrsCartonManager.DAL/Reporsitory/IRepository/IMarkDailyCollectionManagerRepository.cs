using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos.DailyCollectionMark;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IMarkDailyCollectionManagerRepository
    {
        Task<ViewPendingRequestDailyCollection> GetDailyCollectionById(string requestNo);
        Task<PagedResponse<DailyCollectionMarkDto>> SearchDailyCollection(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize);

        bool UpdateDailyCollection(DailyCollectionMarkUpdateDto DailyCollection);
    }
}
