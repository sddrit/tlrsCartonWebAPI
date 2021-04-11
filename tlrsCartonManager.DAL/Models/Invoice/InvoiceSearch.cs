using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models
{
   public  class InvoiceSearch
    {
        [Key]
      
        public string InvoiceId { get; set; }      
        public string CustomerCode { get; set; }

        public string CustomerName{ get; set; }
        public string InvoicePeriod { get; set; }
    }

}
