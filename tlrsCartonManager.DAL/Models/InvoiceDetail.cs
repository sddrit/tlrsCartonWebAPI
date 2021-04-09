using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("InvoiceDetail")]
    public partial class InvoiceDetail
    {
        [Key]
        [Column("trackingId")]
        public long TrackingId { get; set; }
        [Column("invoiceId")]
        [StringLength(50)]
        public string InvoiceId { get; set; }
        [Column("rowId")]
        public int RowId { get; set; }
        [Column("descripton")]
        [StringLength(100)]
        public string Descripton { get; set; }
        [Column("requestType")]
        [StringLength(50)]
        public string RequestType { get; set; }
        [Column("qty")]
        public int? Qty { get; set; }
        [Column("rate", TypeName = "decimal(18, 2)")]
        public decimal? Rate { get; set; }
        [Column("rateType")]
        [StringLength(20)]
        public string RateType { get; set; }
        [Column("amount", TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column("woType")]
        [StringLength(50)]
        public string WoType { get; set; }
        [Column("customerId")]
        public int? CustomerId { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        [InverseProperty(nameof(InvoiceHeader.InvoiceDetails))]
        public virtual InvoiceHeader Invoice { get; set; }
    }
}
