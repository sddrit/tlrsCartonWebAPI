using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Ownership
{
    public class CartonOwnershipTransfer
    { 
        public int? FromCartonNo { get; set; }
        public int? ToCartonNo { get; set; }
        public string SearchBy { get; set; }
        public string ToCustomerCode { get; set; }
        public int? UserId { get; set; }
    }
}
