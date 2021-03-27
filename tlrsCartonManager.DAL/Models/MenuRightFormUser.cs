using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("MenuRightFormUser")]
    public partial class MenuRightFormUser
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("userMenuTrackingId")]
        public int UserMenuTrackingId { get; set; }
        [Column("userFormTrackingId")]
        public int UserFormTrackingId { get; set; }

        [ForeignKey(nameof(UserFormTrackingId))]
        [InverseProperty(nameof(MenuRightForm.MenuRightFormUsers))]
        public virtual MenuRightForm UserFormTracking { get; set; }
        [ForeignKey(nameof(UserMenuTrackingId))]
        [InverseProperty(nameof(MenuRightUser.MenuRightFormUsers))]
        public virtual MenuRightUser UserMenuTracking { get; set; }
    }
}
