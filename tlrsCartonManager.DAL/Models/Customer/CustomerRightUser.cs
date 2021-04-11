using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("CustomerRightUser")]
    public partial class CustomerRightUser
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("userId")]
        public int? UserId { get; set; }
        [Column("customerCode")]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [Column("createdUserId")]
        public int? CreatedUserId { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
    }
}
