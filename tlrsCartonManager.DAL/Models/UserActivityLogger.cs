using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("UserActivityLogger")]
    public partial class UserActivityLogger
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("userId")]
        public int? UserId { get; set; }
        [Column("activityDate", TypeName = "datetime")]
        public DateTime? ActivityDate { get; set; }
        [Column("activityId")]
        public int? ActivityId { get; set; }
        [Column("activityLog")]
        public string ActivityLog { get; set; }
        [Column("activityType")]
        [StringLength(50)]
        public string ActivityType { get; set; }
    }
}
