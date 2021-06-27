using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewModulePermission
    {
        [Column("moduleId")]
        public int? ModuleId { get; set; }
        [Required]
        [Column("moduleName")]
        [StringLength(100)]
        public string ModuleName { get; set; }
        [Column("subModuleId")]
        public int SubModuleId { get; set; }
        [Required]
        [Column("subModuleName")]
        [StringLength(50)]
        public string SubModuleName { get; set; }
        [Column("permission")]
        public int? Permission { get; set; }
    }
}
