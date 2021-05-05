
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Helper;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IUserManagerRepository
    {
        Task<IEnumerable<UserDto>> GetUsersList();
        Task<IEnumerable<WorkerDto>> GetWorkersList();
        Task<PagedResponse<UserSerachDto>> SearchUser(string columnValue, int pageIndex, int pageSize);
    }
}
