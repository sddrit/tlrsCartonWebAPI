using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("InvoiceConfirmation")]
    public partial class InvoiceConfirmation
    {
        [Key]
        [Column("trackingId")]
        public long TrackingId { get; set; }
        [Required]
        [Column("cartonNo")]
        [StringLength(50)]
        public string CartonNo { get; set; }
        [Column("woType")]
        [StringLength(50)]
        public string WoType { get; set; }
        [Column("storageType")]
        public int? StorageType { get; set; }
        [Column("deliveryRouteId")]
        public int? DeliveryRouteId { get; set; }
        [Column("lastRequestNo")]
        [StringLength(20)]
        public string LastRequestNo { get; set; }
        [Column("customerCode")]
        [StringLength(10)]
        public string CustomerCode { get; set; }
        [Column("customerId")]
        public int? CustomerId { get; set; }
        [Column("lastTransactionDate")]
        public int? LastTransactionDate { get; set; }
        [Column("confirmed")]
        public bool? Confirmed { get; set; }
        [Column("confirmedBy")]
        [StringLength(50)]
        public string ConfirmedBy { get; set; }
        [Column("confirmedDate", TypeName = "datetime")]
        public DateTime? ConfirmedDate { get; set; }
        [Column("alternativeCartonNo")]
        [StringLength(20)]
        public string AlternativeCartonNo { get; set; }
        [Column("isNew")]
        public bool? IsNew { get; set; }
        [Column("autoBind")]
        public int? AutoBind { get; set; }
        [Column("createdUserId")]
        public int? CreatedUserId { get; set; }
        [Column("createDate", TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        [Column("luUserId")]
        public int? LuUserId { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }
    }
}
