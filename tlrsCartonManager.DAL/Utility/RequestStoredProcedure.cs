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
			"@requestNumber",
			"@customerId",			
			"@deliveryDate",
			"@ordeReceivedBy",
			"@remark",			
			"@contactPerson",
			"@noOfCartons",			
			"@requestType",
			"@user",
			"@status",
			"@serviceType",
			"@woType",
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
