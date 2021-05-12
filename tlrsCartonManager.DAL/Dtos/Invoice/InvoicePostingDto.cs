using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Invoice
{
    public partial class InvoicePostingDto
    {      
        public long TrackingId { get; set; }      
        public string CustomerCode { get; set; }      
        public string PostingTypeCode { get; set; }       
        public string ReferenceNo { get; set; }      
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
        public int TransactionDateFrom { get; set; }
        public int TransactionDateTo { get; set; }
        public int CreatedUserId { get; set; }      
      
     
    }

}
