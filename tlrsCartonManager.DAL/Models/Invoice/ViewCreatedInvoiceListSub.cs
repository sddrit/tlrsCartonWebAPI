using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Models.GenericReport;

#nullable disable

namespace tlrsCartonManager.DAL.Models
{
    [Keyless]
    public partial class ViewCreatedInvoiceListSub 
    {
        [Required]
        [StringLength(50)]
        [Column("Invoice Id")]
        public string InvoiceId { get; set; }

        [StringLength(20)]
        [Column("Customer Code")]
        public string CustomerCode { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Column("From Date")]
        public int? FromDate { get; set; }

        [Column("To Date")]
        public int? ToDate { get; set; }

        [Column("Invoice Value", TypeName = "numeric(18, 2)")]
        public decimal? InvoiceValue { get; set; }

        [StringLength(100)]
        [Column("Created User")]
        public string CreatedUser { get; set; }

        [StringLength(4000)]
        [Column("Created Date")]
        public string CreatedDate { get; set; }

        [Column("Lu User")]
        [StringLength(100)]
        public string LuUser { get; set; }

        [Required]
        [Column("Lu Date")]
        [StringLength(4000)]
        public string LuDate { get; set; }

        [Column("Customer Address")]
        public string CustomerAddress { get; set; }

        [Column("Contact Person")]
        public string ContactPerson { get; set; }

        [Column("Po No")]
        public string PoNo { get; set; }

        [Column("Vat No")]
        public string VatNo { get; set; }

        [Column("Invoice Tye")]
        public string InvoiceTye { get; set; }


     
    }
}
