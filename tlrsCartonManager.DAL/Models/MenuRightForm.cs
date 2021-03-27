using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("MenuRightForm")]
    public partial class MenuRightForm
    {
        public MenuRightForm()
        {
            MenuRightFormUsers = new HashSet<MenuRightFormUser>();
        }

        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("menuId")]
        public int MenuId { get; set; }
        [Column("formRight")]
        public int FormRight { get; set; }

        [ForeignKey(nameof(MenuId))]
        [InverseProperty(nameof(MenuRight.MenuRightForms))]
        public virtual MenuRight Menu { get; set; }
        [InverseProperty(nameof(MenuRightFormUser.UserFormTracking))]
        public virtual ICollection<MenuRightFormUser> MenuRightFormUsers { get; set; }
    }
}
