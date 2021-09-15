using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.Core.Enums;
using tlrsCartonManager.Core.Environment;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.DAL.Extensions;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Utility;
using static tlrsCartonManager.DAL.Utility.Status;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class UserManagerRepository : IUserManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
       

        public UserManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
         
           
        }

        public async Task<IEnumerable<UserDto>> GetUsersList()
        {
            var users = await _tcContext.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
        public async Task<UserDto> GetUserById(int id)
        {
           var user= _mapper.Map<UserDto>(await _tcContext.Users.
                          Include(x => x.UserRoles).
                          Where(x => x.UserId == id && x.Deleted==false )
                          .FirstOrDefaultAsync());

            return user;
        }
        public async Task<User> GetUserByName(string userName)
        {
            return await _tcContext.Users.Where(x => x.UserName.ToLower() == userName.ToLower() & x.Deleted == false).FirstOrDefaultAsync();

        }
        public async Task<IEnumerable<ViewWorkerUserList>> GetWorkersList()
        {
            return await _tcContext.ViewWorkerUserLists.ToListAsync();

        }
        public int SaveUser(UserDto user, byte[] passwrodHash, byte[] passwordSalt,string trasactionType, int? userId)
        {

            if (trasactionType == TransactionType.Reset.ToString())
            {
                var passwordHistoryList = _tcContext.UserPasswordHistories
                  .Where(x => x.UserId == user.UserId).OrderByDescending(x => x.TrackingId).Take(5).ToList();

                if (trasactionType!=TransactionType.Reset.ToString() && PasswordManager.IsPreviousUsedPassword(passwordHistoryList, user.UserPassword))
                {
                    throw new ServiceException(new ErrorMessage[]
                     {
                            new ErrorMessage()
                            {
                                Message = $"Cannot use old 5 passwrods"
                            }
                     });
                }
            }
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter {ParameterName = UserInsertUpdateDeleteStoredProcedureSearch.StoredProcedureParameters[0].ToString(), Value = user.UserId.AsDbValue() },
                new SqlParameter { ParameterName = UserInsertUpdateDeleteStoredProcedureSearch.StoredProcedureParameters[1].ToString(), Value =user.UserName.AsDbValue() },
                new SqlParameter { ParameterName =UserInsertUpdateDeleteStoredProcedureSearch.StoredProcedureParameters[2].ToString() , Value = user.UserFullName.AsDbValue() },
                new SqlParameter { ParameterName =UserInsertUpdateDeleteStoredProcedureSearch.StoredProcedureParameters[3].ToString() , Value = user.EmpId.AsDbValue() },
                new SqlParameter { ParameterName = UserInsertUpdateDeleteStoredProcedureSearch.StoredProcedureParameters[4].ToString(), Value = user.DepartmentId.AsDbValue() },
                new SqlParameter { ParameterName = UserInsertUpdateDeleteStoredProcedureSearch.StoredProcedureParameters[5].ToString(), Value = passwrodHash.AsDbValue() },
                new SqlParameter { ParameterName =UserInsertUpdateDeleteStoredProcedureSearch.StoredProcedureParameters[6].ToString(),Value= passwordSalt.AsDbValue() },
                new SqlParameter { ParameterName =UserInsertUpdateDeleteStoredProcedureSearch.StoredProcedureParameters[7].ToString(), Value =user.UserRoles!=null? string.Join(",",user.UserRoles.Select(x => x.Id.ToString()).ToArray()): string.Empty },
                new SqlParameter { ParameterName =UserInsertUpdateDeleteStoredProcedureSearch.StoredProcedureParameters[8].ToString() , Value =user.Email.AsDbValue() },
                new SqlParameter { ParameterName =UserInsertUpdateDeleteStoredProcedureSearch.StoredProcedureParameters[9].ToString() , Value =user.Active.AsDbValue() },
                new SqlParameter { ParameterName =UserInsertUpdateDeleteStoredProcedureSearch.StoredProcedureParameters[10].ToString() , Value =trasactionType },
                new SqlParameter { ParameterName =UserInsertUpdateDeleteStoredProcedureSearch.StoredProcedureParameters[11].ToString(), Value = userId.AsDbValue() },
                new SqlParameter { ParameterName =UserInsertUpdateDeleteStoredProcedureSearch.StoredProcedureParameters[12].ToString(), Value =PasswordManagerMobile.EncryptPlainTextToCipherText( user.UserPassword).AsDbValue()},
                  new SqlParameter { ParameterName =UserInsertUpdateDeleteStoredProcedureSearch.StoredProcedureParameters[13].ToString(), Value =user.Lock.AsDbValue()}

            };            

            return _tcContext.Set<IntReturn>().FromSqlRaw(UserInsertUpdateDeleteStoredProcedureSearch.Sql, parms.ToArray()).AsEnumerable().First().Value;
        }
        public async Task<PagedResponse<UserSerachDto>> SearchUser(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = UserStoredProcedureSearch.StoredProcedureParameters[0].ToString(), Value = columnValue==null ? string.Empty :columnValue },
               new SqlParameter { ParameterName = UserStoredProcedureSearch.StoredProcedureParameters[1].ToString(), Value = searchColumn.AsDbValue() },
               new SqlParameter { ParameterName = UserStoredProcedureSearch.StoredProcedureParameters[2].ToString(), Value = sortOrder.AsDbValue() },
               new SqlParameter { ParameterName = UserStoredProcedureSearch.StoredProcedureParameters[3].ToString(), Value = pageIndex },
               new SqlParameter { ParameterName = UserStoredProcedureSearch.StoredProcedureParameters[4].ToString(), Value = pageSize },

            };
            
            var outParam = new SqlParameter { ParameterName = UserStoredProcedureSearch.StoredProcedureParameters[5].ToString(), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            parms.Add(outParam);
            var customerList = await _tcContext.Set<UserSearch>().FromSqlRaw(UserStoredProcedureSearch.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;

            #region paging
            var postResponse = _mapper.Map<List<UserSerachDto>>(customerList);

            var paginationResponse = new PagedResponse<UserSerachDto>
            {
                Data = postResponse,
                pageNumber = pageIndex,
                pageSize = pageSize,
                totalCount = totalRows,
                totalPages = (int)Math.Ceiling(totalRows / (double)pageSize),

            };
            #endregion

            return paginationResponse;
        }
    }
}
