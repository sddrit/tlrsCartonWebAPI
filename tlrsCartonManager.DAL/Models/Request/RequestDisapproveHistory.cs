using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("RequestDisapproveHistory")]
    public partial class RequestDisapproveHistory
    {
        [Key]
        [Column("trackingId")]
        public long TrackingId { get; set; }
        [Column("requestNo")]
        [StringLength(50)]
        public string RequestNo { get; set; }
        [Column("date", TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [Column("reason")]
        public string Reason { get; set; }
        [Column("cartonNos")]
        public string CartonNos { get; set; }
        [Column("createdUserId")]
        public int? CreatedUserId { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
    }
}
