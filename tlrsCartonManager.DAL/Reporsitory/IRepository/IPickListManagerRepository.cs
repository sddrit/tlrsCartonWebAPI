﻿using System;
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
        Task<PickListHeaderDto> GetPickList(string pickListNo, bool isPrint);
        Task<PagedResponse<PickListSearchDto>> SearchPickList(string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize);       
        bool AddPickList (PickListResponseDto pickListInsert);
        TableReturn UpdatePickList(PickListResponseDto pickListInsert);
        TableReturn DeletePickList(PickListResponseDto pickListInsert);
        TableReturn UpdatePickListPrintStatus(PickListResponseDto pickListUpdate);
        object GetPendingPickListSummary(string type);

        TableReturn MarkAsProcessed(PickListResponseDto pickListUpdate);
        Task<List<PickListSummaryDto>> GetPickListSummaryByAssignedUser(string pickListNo);

        Task<PagedResponse<PickListDetailItemDto>> GetPendingPickList(string fromValue, string toValue, string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize, string type);
    }
}
