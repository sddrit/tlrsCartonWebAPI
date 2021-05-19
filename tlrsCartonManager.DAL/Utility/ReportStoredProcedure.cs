using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class InventoryByCustomerStoredProcedure
    {

        public static string StoredProcedureName = "reportInventoryByCustomer";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@customerId",
               "@woType",
               "@asAtDate",
               "@includeSubAccount",
               "@reportType",               
               "@totalRecords"
            };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) + " OUTPUT";

    }
   

}
