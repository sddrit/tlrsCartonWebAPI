using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
   
    public static class RequestStoredProcedure
    {
        public static string StoredProcedureName = "requestInsertUpdateDelete";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
			"@customerId",
			"@priority",
			"@deliveryDate",
			"@ordeReceivedBy",
			"@remark",
			"@customerReference",
			"@contactPerson",
			"@noOfCartons",
			"@remarkCarton",
			"@requestType",
			"@user",
			"@status",
			"@productType",
			"@storageCategory",
			"@contactPersonName",
			"@deliveryLocation",
			"@deliveryRouteId",
			"@statementType",
			"@requestDetail"
			

		};
        public static List<string> StoredProcedureTypeNames = new List<string>()
        {
			"dbo.udtRequestDetail",
            
        };

        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}
	
}
