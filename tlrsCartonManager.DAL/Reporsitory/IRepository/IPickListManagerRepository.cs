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
        Task<PickListHeaderDto> GetPickList(string requestNo);
        Task<PagedResponse<PickListSearchDto>> SearchPickList(string searchText, int pageIndex, int pageSize);
        Task<PagedResponse<PickListDetailItemDto>> GetPendingPickList(string fromValue, string toValue,string searchText, int pageIndex, int pageSize, string type);
        bool AddPickList (PickListResponseDto pickListInsert);
        TableReturn UpdatePickList(PickListResponseDto pickListInsert);
       TableReturn DeletePickList(PickListResponseDto pickListInsert);

        object GetPendingPickListSummary(string type);
    }
}
