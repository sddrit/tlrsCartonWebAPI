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
        public string InvoiceId { get; set; }
        
        [StringLength(20)]
        public string CustomerCode { get; set; }
        
        [StringLength(50)]
        public string Name { get; set; }
        
        public int? FromDate { get; set; }
        
        public int? ToDate { get; set; }
        
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? InvoiceValue { get; set; }
        
        [StringLength(100)]
        public string CreatedUser { get; set; }
        
        [StringLength(4000)]
        public string CreatedDate { get; set; }
        
        [Column("luUser")]
        [StringLength(100)]
        public string LuUser { get; set; }
        
        [Required]
        [Column("luDate")]
        [StringLength(4000)]
        public string LuDate { get; set; }

        [Column("CustomerAddress")]
        public string CustomerAddress { get; set; }

        [Column("ContactPerson")]
        public string ContactPerson { get; set; }

        [Column("PoNo")]
        public string PoNo { get; set; }

        [Column("VatNo")]
        public string VatNo { get; set; }
        
        [Column("InvoiceTye")]
        public string InvoiceTye { get; set; }    



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
             new KeyValuePair<string, string>("Last Updated Date", LuDate)
            };

        }
    }
}
