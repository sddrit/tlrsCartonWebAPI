using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Extensions;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.RoleResponse;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Utility;
using static tlrsCartonManager.DAL.Utility.Status;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class RolePermissionManagerRepository : IRolePermissionManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public RolePermissionManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
        }
        public bool AddRolePermission(RoleResponse response)
        {
           return SaveRolePermission(response, TransactionTypes.Insert.ToString());
        }
        public bool UpdateRolePermission(RoleResponse response)
        {
            return SaveRolePermission(response, TransactionTypes.Update.ToString());
        }
        public bool SaveRolePermission(RoleResponse response,string transactionType)
        {           
            #region Sql Parameter loading
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = UserRoleStoredProcedure.StoredProcedureParameters[0].ToString(),
                    Value = response.RoleId.AsDbValue() },
               
                new SqlParameter { ParameterName = UserRoleStoredProcedure.StoredProcedureParameters[1].ToString(),
                    Value = response.UserId.AsDbValue() },
                new SqlParameter { ParameterName = UserRoleStoredProcedure.StoredProcedureParameters[2].ToString(),
                    Value = transactionType },            
                 new SqlParameter
                {
                   ParameterName = UserRoleStoredProcedure.StoredProcedureParameters[3].ToString(),
                   TypeName = UserRoleStoredProcedure.StoredProcedureTypeNames[0].ToString(),
                   SqlDbType = SqlDbType.Structured,
                   Value =response.RolePermissionList.ToList().ToDataTable()
                }
            };
            #endregion
            return _tcContext.Set<BoolReturn>().FromSqlRaw(UserRoleStoredProcedure.Sql,
                parms.ToArray()).AsEnumerable().First().Value;
        }
        public async Task<IEnumerable<RoleResponseListItem>> GetRoleList()
        {
            var role = await _tcContext.Roles.Where(x=>x.Deleted==0).ToListAsync();
            return _mapper.Map<IEnumerable<RoleResponseListItem>>(role);
        }
        public async Task<IEnumerable<ViewUserRole>> GetPermissionPendingRoleList()
        {
            var role = await _tcContext.ViewUserRoles.ToListAsync();
            return _mapper.Map<IEnumerable<ViewUserRole>>(role);
        }
        public async Task<IEnumerable<RolePermissionListItem>> GeRolePermissionList()        {
          
            return _mapper.Map<IEnumerable<RolePermissionListItem>>( await _tcContext.ViewMenus.ToListAsync());
          
        }
        public async Task<bool> AddRole(Role role)
        {
            role.Active = true;
            role.Deleted = 0;
            role.CreatedDate = System.DateTime.Today;

             _tcContext.Roles.Add(role);
            if (await _tcContext.SaveChangesAsync() > 0)return true;          
            else return false;
                      
        }
        public async Task<bool> DeleteRole(Role role)
        {
            var currentRole = _tcContext.Roles.Where(x => x.Id == role.Id).FirstOrDefault();
            currentRole.Deleted = 1;
            _tcContext.Roles.Update(currentRole);
            if (await _tcContext.SaveChangesAsync() > 0) return true;
            else return false;

        }
        public string ValidateRole(Role role)
        {
            if (_tcContext.Roles.Where(x => x.Description == role.Description && x.Deleted==0).FirstOrDefault()!=null)
                return "Role already exist";

            return string.Empty;

        }

        public async Task<IEnumerable<RolePermissionListItem>> GetRolePermissionListById(int id)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = UserRoleByIdStoredProcedure.StoredProcedureParameters[0].ToString(),
                    Value = id.AsDbValue() }                           
            };

            return await _tcContext.Set<RolePermissionListItem>().FromSqlRaw(UserRoleByIdStoredProcedure.Sql,
                parms.ToArray()).ToListAsync();
        }
    }
}
