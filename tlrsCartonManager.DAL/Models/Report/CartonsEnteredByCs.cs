using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Report
{
    public class CartonsEnteredByCs
    {
        public string RequestType { get; set; }
        public int CartonCount { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
