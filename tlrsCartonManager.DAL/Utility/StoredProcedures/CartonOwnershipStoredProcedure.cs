using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class CartonOwnershipSummaryStoredProcedure
    {

        public static string StoredProcedureName = "ownershipSearchSummary";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
                "@valueFrom",
                "@valueTo",
                "@searchBy"
                
         };

        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }
    public class CartonOwnershipTransferStoredProcedure
    {
        public static string StoredProcedureName = "ownershipTransfer";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
            "@valueFrom",
            "@valueTo",
            "@searchBy",
            "@toCustomerCode",
            "@userId"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }
    public class CartonOwnershipCustomerListStoredProcedure
    {

        public static string StoredProcedureName = "ownershipSearchCustomerList";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
                "@valueFrom",
                "@valueTo",
                "@searchBy"

         };

        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }
}
