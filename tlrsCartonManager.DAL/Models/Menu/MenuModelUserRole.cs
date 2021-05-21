using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("MenuUserRole")]
    public partial class MenuModelUserRole
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("menuId")]
        public int MenuId { get; set; }
        [Column("roleId")]
        public int RoleId { get; set; }

        [ForeignKey(nameof(MenuId))]
        [InverseProperty(nameof(MenuModel.MenuModelUserRoles))]
        public virtual MenuModel Model { get; set; }
    }
}
