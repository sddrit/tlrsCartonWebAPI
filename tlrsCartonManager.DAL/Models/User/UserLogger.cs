using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("UserLogger")]
    public partial class UserLogger
    {
        [Key]
        [Column("trackingId")]
        public long TrackingId { get; set; }
        [Column("userId")]
        public int? UserId { get; set; }
        [Column("passwordCreatedDate", TypeName = "datetime")]
        public DateTime? LoginDate { get; set; }
        [Column("passwordExpiryDate", TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
    }
}
