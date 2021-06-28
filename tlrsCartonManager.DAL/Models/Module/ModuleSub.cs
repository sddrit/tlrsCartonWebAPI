using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("ModuleSub")]
    public partial class ModuleSub
    {
        public ModuleSub()
        {
            ModulePermissions = new HashSet<ModulePermission>();
        }

        [Key]
        [Column("id")]
        public int SubModuleId { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string SubModuleName { get; set; }
        [Column("moduleId")]
        public int? ModuleId { get; set; }
        public virtual Module Module { get; set; }      
        public virtual ICollection<ModulePermission> ModulePermissions { get; set; }
    }
}
