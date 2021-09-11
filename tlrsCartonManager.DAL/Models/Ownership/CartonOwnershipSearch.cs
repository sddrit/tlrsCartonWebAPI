using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Ownership
{
    public class CartonOwnershipSearch
    {
        public int CartonNo { get; set; }
        public string WONumber { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string AlternativeCartonNo { get; set; }
        public string Status { get; set; }
        public string LastConfirmedStatus { get; set; }
    }
}
