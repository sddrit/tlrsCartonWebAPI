using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos
{
    public class InvoiceConfirmationDto
    {
        [Key]
        public long TrackingId { get; set; }
        public string CartonNo { get; set; }
        public string WoType { get; set; }
        public int? StorageType { get; set; }
        public int? DeliveryRouteId { get; set; }
        public string LastRequestNo { get; set; }
        public string CustomerCode { get; set; }
        public int? CustomerId { get; set; }
        public int? LastTransactionDate { get; set; }
        public bool? Confirmed { get; set; }
        public string ConfirmedBy { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public string AlternativeCartonNo { get; set; }
        public bool? IsNew { get; set; }
        public int? AutoBind { get; set; }

    }
    public class InvoiceConfirmationSearchDto
    {
        public string RequestNo { get; set; }
        public int RequestDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string WOType { get; set; }
    }
}
