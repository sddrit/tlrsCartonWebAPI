using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models
{
    public class UserPermission
    {
        //public int? ModuleId { get; set; }
        //public string ModuleName { get; set; }
        //public int? SubModuleId { get; set; }
        public string SubModuleName { get; set; }
        public int[] Permissions { get; set; }
    }

    public class UserPermissionItem
    {
        //public int? ModuleId { get; set; }
        //public string ModuleName { get; set; }
        //public int? SubModuleId { get; set; }
        public string SubModuleName { get; set; }
        public int Permission { get; set; }
    }
}
