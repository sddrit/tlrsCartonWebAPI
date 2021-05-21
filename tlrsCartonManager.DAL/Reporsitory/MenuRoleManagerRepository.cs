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
            var role = await _tcContext.Roles.ToListAsync();
            return _mapper.Map<IEnumerable<RoleResponseListItem>>(role);
        }

        public async Task<IEnumerable<MenuListItem>> GetMenuList()
        {
            return _mapper.Map<IEnumerable<MenuListItem>>( await _tcContext.ViewMenus.ToListAsync());
          
        }
    }
}
