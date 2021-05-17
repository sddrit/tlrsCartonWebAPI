using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("RequestDetail")]
    public partial class RequestDetail
    {
        [Key]
        [Column("trackingId")]
        public long TrackingId { get; set; }      
        [Required]
        [Column("requestNo")]
        [StringLength(20)]
        public string RequestNo { get; set; }
        [Column("cartonNo")]
        public int CartonNo { get; set; }
        [Column("disposalDate")]
        public int? DisposalDate { get; set; }
        [Column("disposalTimeFrame")]
        public int? DisposalTimeFrame { get; set; }
        [Column("fromMobile")]
        public bool? FromMobile { get; set; }
        [Column("picked")]
        public bool? Picked { get; set; }
        [Column("pickListNo")]
        [StringLength(50)]
        public string PickListNo { get; set; }
        [Column("status")]
        public int? Status { get; set; }
        [Column("feedBack")]
        [StringLength(500)]
        public string FeedBack { get; set; }
        [Column("statusInDate", TypeName = "datetime")]
        public DateTime? StatusInDate { get; set; }     
              
        [Column("deleted")]
        public bool? Deleted { get; set; }

        [ForeignKey(nameof(RequestNo))]
        [InverseProperty(nameof(RequestHeader.RequestDetails))]
        public virtual RequestHeader Request { get; set; }
    }
}
