using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Helper;

namespace tlrsCartonManager.DAL.Models.Report
{
    public class InventoryByCustomer
    {        
        public int CartonNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string WoType { get; set; }
        public string LocationCode { get; set; }
        public string LastRequestNo { get; set; }
        public string FirstInDate { get; set; }
        public string DisposalDate { get; set; }
        public string TransactionDate { get; set; }
        public string StorageType { get; set; }
        public string WareHouseCode { get; set; }
        public string RetrievalType { get; set; }
        
    }
    public class InventoryByCustomerSummary
    {
        public string WoType { get; set; }
        public int CartonCount { get; set; }

    }
    public class InventoryByRetreivalSummary
    {
        public string WoType { get; set; }
        public int CartonCount { get; set; }

    }
    public class InventoryByCustomerReponse
    {
        public List<InventoryByCustomer> InventoryList { get; set; }
        public List<InventoryByCustomerSummary> InventorySummary { get; set; }
        public List<InventoryByRetreivalSummary> RetreivalSummary { get; set; }


    }
}
