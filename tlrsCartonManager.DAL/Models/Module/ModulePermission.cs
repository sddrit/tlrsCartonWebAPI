using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("ModulePermission")]
    public partial class ModulePermission
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("subModuleId")]
        public int SubModuleId { get; set; }
        [Column("permissionId")]
        public int PermissionId { get; set; }

        [ForeignKey(nameof(SubModuleId))]
        [InverseProperty(nameof(ModuleSub.ModulePermissions))]
        public virtual ModuleSub SubModule { get; set; }
    }
}
