using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("InvoiceHeader")]
    public partial class InvoiceHeader
    {
        public InvoiceHeader()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        [Key]
        [Column("invoiceId")]
        [StringLength(50)]
        public string InvoiceId { get; set; }
        [Column("trackingId")]
        public long TrackingId { get; set; }
        [Column("customerId")]
        public int? CustomerId { get; set; }
        [Column("fromDate")]
        public int? FromDate { get; set; }
        [Column("toDate")]
        public int? ToDate { get; set; }
        [Column("invoiceValue", TypeName = "numeric(18, 2)")]
        public decimal? InvoiceValue { get; set; }
        [Column("invoiceProfielId")]
        public int? InvoiceProfielId { get; set; }
        [Column("createdUserId")]
        public int? CreatedUserId { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column("luUserId")]
        [StringLength(50)]
        public int? LuUserId { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }

        [InverseProperty(nameof(InvoiceDetail.Invoice))]
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
