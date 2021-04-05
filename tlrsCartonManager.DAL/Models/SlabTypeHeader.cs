using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("SlabTypeHeader")]
    public partial class SlabTypeHeader
    {
        public SlabTypeHeader()
        {
            SlabTypeDetails = new HashSet<SlabTypeDetail>();
        }

        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("description")]
        [StringLength(50)]
        public string Description { get; set; }
        [Column("calucationType")]
        public int? CalucationType { get; set; }
        [Column("invoiceChargingType")]
        public int? InvoiceChargingType { get; set; }
        [Column("invoiceProfileId")]
        public int? InvoiceProfileId { get; set; }
        [Column("cartonType")]
        public int? CartonType { get; set; }
        [Column("active")]
        public bool? Active { get; set; }
        [Column("deleted")]
        public bool? Deleted { get; set; }
        [Column("createdUser")]
        public int? CreatedUser { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column("luUser")]
        public int? LuUser { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }

        [ForeignKey(nameof(InvoiceProfileId))]
        [InverseProperty("SlabTypeHeaders")]
        public virtual InvoiceProfile InvoiceProfile { get; set; }
        [InverseProperty(nameof(SlabTypeDetail.IdNavigation))]
        public virtual ICollection<SlabTypeDetail> SlabTypeDetails { get; set; }
    }
}
