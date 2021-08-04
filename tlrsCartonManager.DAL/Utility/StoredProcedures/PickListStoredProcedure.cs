using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
   
    public static class PickListStoredProcedure
	{
        public static string StoredProcedureName = "pickListInsertUpdateDelete";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
            "@pickListNo",
            "@statementType",
            "@userId",
            "@deviceId",
            "@pickList"


        };
        public static List<string> StoredProcedureTypeNames = new List<string>()
        {
            "dbo.udtPickList",
            
        };

        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}


    public static class PickListSummaryStoredProcedure
    {
        public static string StoredProcedureName = "pickListPendingSummary";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
            "@type"


        };     

        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
    }

}
