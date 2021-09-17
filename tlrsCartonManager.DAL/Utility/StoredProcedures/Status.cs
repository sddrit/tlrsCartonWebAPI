using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public static class Status
    {
        public enum TransactionStatus
        {
            None,
            Active,
            InActive,
            Disable
        }
        public enum TransactionTypes
        {
            Insert,
            Update,
            Delete,
            Print,
            Complete
        }
        public enum SearchCriterias
        {
            CSummary,
            SSummary,
            FSummary,
            BSummary,
            VSummary,
            PSummary,
            RDetail
        }
        public enum InventoryReportTypes
        {
           Detail,
           SummaryInventory,
           SummaryRetreival

        }
       
    }
}
