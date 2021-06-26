using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewMenu
    {
        [Column("moduleId")]
        public int ModuleId { get; set; }

        [Required]
        [Column("moduleName")]       
        public string ModuleName { get; set; }

        [Column("subModuleId")]        
        public int? SubModuleId { get; set; }

        [Column("subModuleName")]
        public string SubModuleName { get; set; }
    }
}
