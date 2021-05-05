using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("MenuModelOptionsUserRole")]
    public partial class MenuModelOptionsUserRole
    {
        [Key]
        [Column("TrackingID")]
        public int TrackingId { get; set; }
        [Column("UserRoleModelID")]
        public int UserRoleModelId { get; set; }
        [Column("FormRightID")]
        public int FormRightId { get; set; }

        [ForeignKey(nameof(FormRightId))]
        [InverseProperty(nameof(MenuRightFormName.MenuModelOptionsUserRoles))]
        public virtual MenuRightFormName FormRight { get; set; }
    }
}
