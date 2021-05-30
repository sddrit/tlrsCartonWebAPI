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
    public class MenuRoleManagerRepository : IMenuRoleManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public MenuRoleManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
        }

        public bool AddRole(RoleResponse response)
        {
            List<MenuActionRoleUtd> lstMenuActionRole = new List<MenuActionRoleUtd>();
            foreach(var item in response.MenuRoleList)
            {    
                foreach(var item2 in item.MenuActionRoleList)
                {
                    MenuActionRoleUtd menuRoleUtd = new MenuActionRoleUtd()
                    {
                        MenuId = item.MenuId,                       
                    };
                    menuRoleUtd.ActionId = item2.ActionId;
                    lstMenuActionRole.Add(menuRoleUtd);
                }
            }
            #region Sql Parameter loading
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = UserRoleStoredProcedure.StoredProcedureParameters[0].ToString(),
                    Value = response.RoleId.AsDbValue() },
                new SqlParameter { ParameterName = UserRoleStoredProcedure.StoredProcedureParameters[1].ToString(),
                    Value = response.RoleName.AsDbValue() },
                new SqlParameter { ParameterName = UserRoleStoredProcedure.StoredProcedureParameters[2].ToString(),
                    Value = response.UserId.AsDbValue() },
                new SqlParameter { ParameterName = UserRoleStoredProcedure.StoredProcedureParameters[3].ToString(),
                    Value = TransactionTypes.Insert.ToString()},             
                 new SqlParameter
                {
                   ParameterName = UserRoleStoredProcedure.StoredProcedureParameters[4].ToString(),
                   TypeName = UserRoleStoredProcedure.StoredProcedureTypeNames[0].ToString(),
                   SqlDbType = SqlDbType.Structured,
                   Value =lstMenuActionRole.ToDataTable()
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
        public async Task<IEnumerable<ViewUserRole>> GetUserRoleList()
        {
            var role = await _tcContext.ViewUserRoles.ToListAsync();
            return _mapper.Map<IEnumerable<ViewUserRole>>(role);
        }
        public async Task<IEnumerable<MenuListItem>> GetMenuList()
        {
            return _mapper.Map<IEnumerable<MenuListItem>>( await _tcContext.ViewMenus.ToListAsync());
          
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
    }
}
