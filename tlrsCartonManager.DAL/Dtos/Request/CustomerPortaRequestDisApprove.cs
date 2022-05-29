using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Dtos.Request
{
    public class CustomerPortaRequestDisApprove
    {
        public string RequestNumber { get; set; }
        public string RequestType { get; set; }
        public string Reason { get; set; }
    }
}
