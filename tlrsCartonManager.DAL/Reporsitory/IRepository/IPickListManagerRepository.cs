using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Dtos.Pick;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IPickListManagerRepository
    {
        Task<PickListDto> GetPickList(string requestNo);
        Task<PagedResponse<PickListSearchDto>> SearchPickList(string searchText, int pageIndex, int pageSize);
        TableResponse<TableReturn> AddPickList (List<PickListDto> pickListInsert);
        TableResponse<TableReturn> UpdatePickList(string pickListNo, int userId, string deviceId);
        TableResponse<TableReturn> DeletePickList(string pickListNo, int userId);
    }
}
