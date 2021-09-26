using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Verification
{
    public class VerificationPickViewModel
    {
        public string CartonNo { get; set; }
        public string LocationCode { get; set; }
        public bool IsVerified { get; set; }
    }

    public class VerificationPickInvalidViewModel
    {
        public string CartonNo { get; set; }
        public string LocationCode { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
    }

    public class VerificationPickModel    {
        public string CartonNo { get; set; }
        public string RequestNo { get; set; }       
    }

}
