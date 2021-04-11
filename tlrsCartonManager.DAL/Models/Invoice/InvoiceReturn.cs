using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models
{
    public class InvoiceReturn
    {
        [Key]
        public string InvoiceId { get; set; }
        public decimal? InvoiceValue { get; set; }
        public bool  IsSeparateInvoice { get; set; }
        public string InvoiceType { get; set; }
        public string ContactPersonInv { get; set; }
        public string PONo { get; set; }
    }
}
