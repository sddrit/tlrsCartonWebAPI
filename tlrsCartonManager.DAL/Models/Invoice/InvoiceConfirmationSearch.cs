using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models
{
    public class InvoiceConfirmationSearch
    {
        [Key]
            public string RequestNo { get; set; }
            public int RequestDate { get; set; }
            public string CustomerCode { get; set; }
            public string CustomerName { get; set; }
            public string WOType { get; set; }       
    }
}
