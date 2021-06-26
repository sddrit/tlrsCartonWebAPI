using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Menu
{
    public class MenuModelsDto
    {
        public int ModelID { get; set; }

        public string ModelName { get; set; }

        public ICollection<MenuModelOptionsDto> ModelOptions { get; set; }

    }

    public class UserModulePermission
    {
        public int? ModuleId  { get; set; }
        public string ModuleName { get; set; }
        public int? SubModuleId { get; set; }
        public string SubModuleName { get; set; }
        public int? Permission { get; set; }
    }

    public class UserRolePermission
    {       
        public int? SubModuleId { get; set; }        
        public int? Permission { get; set; }
    }
}
