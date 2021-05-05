using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("MenuModelUserRole")]
    public partial class MenuModelUserRole
    {
        [Key]
        [Column("TrackingID")]
        public int TrackingId { get; set; }
        [Column("ModelID")]
        public int ModelId { get; set; }
        [Column("UserRoleID")]
        public int UserRoleId { get; set; }

        [ForeignKey(nameof(ModelId))]
        [InverseProperty(nameof(MenuModel.MenuModelUserRoles))]
        public virtual MenuModel Model { get; set; }
    }
}
