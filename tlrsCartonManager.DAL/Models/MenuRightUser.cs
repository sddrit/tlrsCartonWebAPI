using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("MenuRightUser")]
    public partial class MenuRightUser
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("menuId")]
        public int MenuId { get; set; }
        [Column("userId")]
        public int UserId { get; set; }

        [ForeignKey(nameof(MenuId))]
        [InverseProperty(nameof(MenuRight.MenuRightUsers))]
        public virtual MenuRight Menu { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("MenuRightUsers")]
        public virtual User User { get; set; }
    }
}
