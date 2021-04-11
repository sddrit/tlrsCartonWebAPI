using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("InvoiceSlabTypeDetail")]
    public partial class InvoiceSlabTypeDetail
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }
        [Column("id")]
        public int? Id { get; set; }
        [Column("rowId")]
        public int? RowId { get; set; }
        [Column("fromSlab")]
        public int? FromSlab { get; set; }
        [Column("toSlab")]
        public int? ToSlab { get; set; }
        [Column("rate", TypeName = "decimal(18, 2)")]
        public decimal? Rate { get; set; }
        public int InvoiceSlabTypeHeadertrackingId { get; set; }

        [ForeignKey(nameof(Id))]
        [InverseProperty(nameof(InvoiceSlabTypeHeader.InvoiceSlabTypeDetails))]
        public virtual InvoiceSlabTypeHeader IdNavigation { get; set; }
    }
}
