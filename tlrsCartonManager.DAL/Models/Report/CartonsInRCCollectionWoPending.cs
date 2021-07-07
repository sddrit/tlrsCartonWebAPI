using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Report
{
    public class CartonsInRCCollectionWoPending
    {
        public string CartonNo { get; set; }
        public string Status { get; set; }
        public string LastConfirmedStatus { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public int? LastTransactionDate { get; set; }
        public int? LastUpdatedDate { get; set; }
        public string LastRequestNo { get; set; }
        public string LocationCode { get; set; }
        

    }
}
