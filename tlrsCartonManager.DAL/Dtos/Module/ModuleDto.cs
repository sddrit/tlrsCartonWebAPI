﻿using System;
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
    public class ModuleSubMetaDataDto
    {              
        public int SubModuleId { get; set; }
        public string SubModuleName { get; set; }
        public int ModuleId { get; set; }
        public bool Active { get; set; }
    }
    public class ModuleMetaDataDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

    }
}
