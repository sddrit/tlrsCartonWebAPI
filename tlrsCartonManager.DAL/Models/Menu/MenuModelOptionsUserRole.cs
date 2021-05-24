using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("MenuUserRoleAction")]
    public partial class MenuModelOptionsUserRole
    {
        [Key]
        [Column("trackingID")]
        public int TrackingId { get; set; }
        [Column("userRoleId")]
        public int UserRoleId { get; set; }
        [Column("actionId")]
        public int ActionId { get; set; }

        [ForeignKey(nameof(ActionId))]
        [InverseProperty(nameof(MenuRightFormName.MenuModelOptionsUserRoles))]
        public virtual MenuRightFormName FormRight { get; set; }
    }
}
