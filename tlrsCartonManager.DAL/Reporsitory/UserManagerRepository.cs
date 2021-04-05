using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Utility;

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
        public async Task<PagedResponse<UserSerachDto>> SearchUser(string columnValue, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter { ParameterName = UserStoredProcedureSearch.StoredProcedureParameters[0].ToString(), Value = columnValue==null ? string.Empty :columnValue },

               new SqlParameter { ParameterName = UserStoredProcedureSearch.StoredProcedureParameters[1].ToString(), Value = pageIndex },
               new SqlParameter { ParameterName = UserStoredProcedureSearch.StoredProcedureParameters[2].ToString(), Value = pageSize },

            };
            var outParam = new SqlParameter { ParameterName = UserStoredProcedureSearch.StoredProcedureParameters[3].ToString(), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
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
