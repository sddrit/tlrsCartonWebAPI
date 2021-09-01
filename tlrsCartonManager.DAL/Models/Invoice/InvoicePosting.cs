using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Table("InvoicePosting")]
    public partial class InvoicePosting
    {
        [Key]
        [Column("trackingId")]
        public long TrackingId { get; set; }
        [Required]
        [Column("customerCode")]
        [StringLength(20)]
        public string CustomerCode { get; set; }
        [Required]
        [Column("postingTypeCode")]
        [StringLength(20)]
        public string PostingTypeCode { get; set; }
        [Column("referenceNo")]
        [StringLength(50)]
        public string ReferenceNo { get; set; }
        [Column("qty")]
        public int Qty { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }
        [Column("percentage")]
        public decimal Percentage { get; set; }
        [Column("transactionDateFrom")]
        public int TransactionDateFrom { get; set; }
        [Column("transactionDateTo")]
        public int TransactionDateTo { get; set; }
        [Column("createdUserId")]
        public int CreatedUserId { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column("luUserId")]
        public int? LuUserId { get; set; }
        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }
    }
    public class InvoicePostingSearch
    {
      
        public string CustomerCode { get; set; }
        public string Name { get; set; }      
        public string Description { get; set; }
        public string ReferenceNo { get; set; }

        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
        public int TransactionDateFrom { get; set; }
        public int TransactionDateTo { get; set; }

        public int TrackingId { get; set; }

        public bool Invisible { get; set; }
    }
}
