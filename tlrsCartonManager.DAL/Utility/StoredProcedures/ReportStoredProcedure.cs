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
    public class ToBeDisposedCartonListStoredProcedure
    {
        public static string StoredProcedureName = "reportToBeDisposedCartonList";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@customerCode",
               "@includeSubAccount"

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }
    public class RetentionTrackerStoredProcedure
    {
        public static string StoredProcedureName = "reportRetentionTracker";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@customerCode",
               "@includeSubCustomer",
               "@asofDate"

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }
    public class RetentionTrackerDisposalStoredProcedure
    {
        public static string StoredProcedureName = "reportRetentionTrackerDisposal";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@customerCode",
               "@includeSubCustomer",
               "@fromDate",
               "@toDate"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }
    public class RetreivalTrackerStoredProcedure
    {
        public static string StoredProcedureName = "reportRetreivalTracker";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@customerCode",
               "@includeSubCustomer",
               "@fromDate",
               "@toDate"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }
    public class LongOutStandingStoredProcedure
    {
        public static string StoredProcedureName = "reportLongOutstanding";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@customerCode",
               "@includeSubCustomer",
               "@asofDate"

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }

    public class InventorySummaryAsAtDate
    {
        public static string StoredProcedureName = "reportInventoryAsAtDate";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@asAtDate"

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }
    public class CartonsInRCCollectionWoPendingStoredProcedure
    {
        public static string StoredProcedureName = "reportCartonsInRCCollectionWoPending";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@asAtDate",
               "@isAsAtDate"

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }

    public class CartonsInRCWoPendingStoredProcedure
    {
        public static string StoredProcedureName = "reportCartonsInRCWoPending";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@asAtDate"

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }
    public class DailyPalletedSummaryStoredProcedure
    {
        public static string StoredProcedureName = "reportDailyPalletedSummary";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@asAtDate",
               "@toDate",
               "@locationCode"

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }

    public class CartonEnteredByCsStoredProcedure
    {
        public static string StoredProcedureName = "reportCartonEnteredByCs";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@fromDate",
               "@toDate"

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }

    public class DateWiseCollectionSummaryByCustomerStoredProcedure
    {
        public static string StoredProcedureName = "reportDateWiseCollectionSummaryByCustomer";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@fromDate",
               "@toDate"

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }
    public class InvoiceNotGeneratedCustomerListStoredProcedure
    {
        public static string StoredProcedureName = "reportInvoiceNotGeneratedCustomerList";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@fromDate",
               "@toDate",
               "@billingCycle"

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }


}
