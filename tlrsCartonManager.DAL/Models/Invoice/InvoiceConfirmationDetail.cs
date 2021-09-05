using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Invoice
{
    public class InvoiceConfirmationDetail
    {
        [Key]
        public int CartonNo { get; set; }       
        public bool? Picked { get; set; }
        public string PickListNo { get; set; }
        public string LocationCode { get; set; }
        public int ToCartonNo { get; set; }
        public int CartonCount { get; set; }

    }
}
