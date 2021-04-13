using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Invoice
{
    public class InvoiceSlabTypeHeaderDto
    {
        [Key]
        public int TrackingId { get; set; }
        public string Description { get; set; }
        public int? CalucationType { get; set; }
        public int RouteCode { get; set; }
        public int? InvoiceChargingType { get; set; }
        public int InvoiceProfileId { get; set; }
        public int? CartonType { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LuUser { get; set; }
        public DateTime? LuDate { get; set; }
        public int InvoiceProfileprofileid { get; set; }
        public virtual ICollection<InvoiceSlabTypeDetailDto> InvoiceSlabTypeDetails { get; set; }


    }
}
