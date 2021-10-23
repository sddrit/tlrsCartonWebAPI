using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Report
{
    public class DateWiseCollectionSummaryByCustomer
    {
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public int LastTransactionDate { get; set; }
        public bool IsNew { get; set; }
        public int? CartonCount { get; set; }

    }
}
