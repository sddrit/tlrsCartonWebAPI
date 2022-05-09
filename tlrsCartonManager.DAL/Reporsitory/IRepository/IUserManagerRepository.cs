
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IUserManagerRepository
    {
        Task<IEnumerable<UserDto>> GetUsersList();
        Task<IEnumerable<ViewWorkerUserList>> GetWorkersList();
        Task<UserDto> GetUserById(int id);
        Task<PagedResponse<UserSerachDto>> SearchUser(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize);
      
        Task<User> GetUserByName(string userName);
        int SaveUser(UserDto user, byte[] passwrodHash, byte[] passwordSalt, string trasactionType, int? userId);

        Task<PagedResponse<UserSerachCustomerPortalDto>> SearchUserCustomerPortal(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize);
    }
}
