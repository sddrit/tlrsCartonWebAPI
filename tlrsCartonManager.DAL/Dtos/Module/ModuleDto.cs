using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Module
{
    public class SubModuleDto
    {      
        public int ModuleId { get; set; }        
        public string ModuleName { get; set; }
        public int SubModuleId { get; set; }
        public string SubModuleName { get; set; }
        public List<int> ModulePermissions { get; set; }
    }
    public class ModuleDto
    {
        public int ModuleId { get; set; }      
        public string ModuleName { get; set; }

    }
    public class ModulePermissionDto
    {
        public int PermissionId { get; set; }


    }
}
