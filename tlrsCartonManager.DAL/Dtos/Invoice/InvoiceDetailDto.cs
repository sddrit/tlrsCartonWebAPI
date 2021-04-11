using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class InvoiceDetailDto
    {
        [Key]
        public long TrackingId { get; set; }
        public string InvoiceId { get; set; }
        public int RowId { get; set; }
        public string Descripton { get; set; }
        public string RequestType { get; set; }
        public int? Qty { get; set; }
        public decimal? Rate { get; set; }
        public string RateType { get; set; }
        public decimal? Amount { get; set; }
        public string WoType { get; set; }
        public int? CustomerId { get; set; }

    }
}
