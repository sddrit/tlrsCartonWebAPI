using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Invoice
{
    public class InvoiceProfileDto
    {
        [Key]
        public int ProfileId { get; set; }
        public string ProfileDesc { get; set; }
        public int ProfileType { get; set; }
        public int Active { get; set; }
        public int Deleted { get; set; }
        public virtual ICollection<InvoiceSlabTypeHeaderDto> InvoiceSlabTypeHeaders { get; set; }
    }
}
