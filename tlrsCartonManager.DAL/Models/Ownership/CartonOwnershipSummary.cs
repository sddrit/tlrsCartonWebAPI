using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;

namespace tlrsCartonManager.DAL.Models.Ownership
{
    public class CartonOwnershipSummary
    {
        public int TotalCartonCount { get; set; }
        public string CustomerCodes { get; set; }
        
    }
}
