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
    public interface IReminderManagerRepository
    {
        Task<ReminderDto> GetReminderListById(string requestNo);
        Task<PagedResponse<ReminderDto>> SearchReminders(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize);
        bool UpdateReminders(ReminderUpdateDto request);
    }
}
