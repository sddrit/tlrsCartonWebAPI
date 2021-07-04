using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models.Report
{
    public class InventorySummaryAsAtdate
    {

        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public int EmptyCartonCount { get; set; }
        public int CollectionCartonCount { get; set; }
        public int RetrievalCartonCount { get; set; }
        public int PermOutCartonCount { get; set; }
        public int DisposalCartonCount { get; set; }



    }
}
