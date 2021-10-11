using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("TaxEffectiveDate")]
    public partial class TaxEffectiveDate
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("taxCode")]
        [StringLength(20)]
        public string TaxCode { get; set; }
        [Column("effectiveFromDate")]
        public int EffectiveFromDate { get; set; }
        [Column("effectiveToDate")]
        public int? EffectiveToDate { get; set; }
        [Column("rate", TypeName = "decimal(18, 2)")]
        public decimal Rate { get; set; }
        [Column("showTaxInfoOnInvoice")]
        public bool ShowTaxInfoOnInvoice { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; }
        [Column("createdUserId")]
        public int CreatedUserId { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        public decimal? MinAmount { get; set; }
        public string RateType { get; set; }

    }
}
