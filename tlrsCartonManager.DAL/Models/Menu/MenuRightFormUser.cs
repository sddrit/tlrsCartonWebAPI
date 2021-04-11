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
        [Column("formRightId")]
        public int? FormRightId { get; set; }

        [ForeignKey(nameof(UserMenuTrackingId))]
        [InverseProperty(nameof(MenuRightAttachedUser.MenuRightFormUsers))]
        public virtual MenuRightAttachedUser UserMenuTracking { get; set; }
    }
}
