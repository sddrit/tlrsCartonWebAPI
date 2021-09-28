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
    public partial class ViewCreatedInvoiceList : IGenericReportDataItem
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

        [Column("Invoice Type")]
        public string InvoiceTye { get; set; }

        [Column("Svat No")]
        public string SvatNo { get; set; }

        [Column("Vat Percentage")]
        public decimal? VatPercentage { get; set; }
        public string Status { get; set; }
        public IList<KeyValuePair<string, string>> GetValues()
        {
            return new List<KeyValuePair<string, string>>()
            {
             new KeyValuePair<string, string>("Invoice Id", InvoiceId),
             new KeyValuePair<string, string>("Customer Code", CustomerCode),
             new KeyValuePair<string, string>("Name", Name),
             new KeyValuePair<string, string>("From Date", FromDate.ToString()),
             new KeyValuePair<string, string>("To Date", ToDate.ToString()),
             new KeyValuePair<string, string>("Invoice Value", InvoiceValue.ToString()),
             new KeyValuePair<string, string>("Created User", CreatedUser),
             new KeyValuePair<string, string>("Created Date", CreatedDate),
             new KeyValuePair<string, string>("Last Updated User", LuUser),
             new KeyValuePair<string, string>("Last Updated Date", LuDate),
             new KeyValuePair<string, string>("Status", Status)
            };

        }
    }
}
