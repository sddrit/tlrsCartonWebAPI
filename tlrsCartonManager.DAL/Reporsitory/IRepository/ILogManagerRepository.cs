using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Dtos.Menu;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.SystemLogs;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface ILogManagerRepository
    {
        PagedResponse<Log> GetDbErrorLogAsync(string searchColumn, int pageIndex, int pageSize);

        Task<PagedResponse<AuditTrailUserActivityModel>> SearchUserActivity(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize);

        Task<PagedResponse<AuditTrailUserLoginModel>> SearchUserLogin(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize);
    }
}
