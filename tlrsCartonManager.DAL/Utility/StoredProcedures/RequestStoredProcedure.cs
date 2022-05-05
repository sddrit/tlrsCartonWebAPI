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
			"@customerCode",			
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
			"@requestDetail",
			"@storageType",
			"@contactNo",
			"@priority",
			"@type",
			"@processStaus"


		};
        public static List<string> StoredProcedureTypeNames = new List<string>()
        {
			"dbo.udtRequestDetail",
            
        };

        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}

	public static class AddDocketStoredProcedure
	{
		public static string StoredProcedureName = "originalDocketInsert";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@requestNo",
			"@docketNo",
			"@userId"	
		};	
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}
	public static class RequestValidateStoredProcedure
	{
		public static string StoredProcedureName = "requestInsertUpdateDeleteValidate";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			
			"@customerCode",			
			"@requestType",
			"@requestNo",
			"@statementType",
			"@requestDetail"

		};
		public static List<string> StoredProcedureTypeNames = new List<string>()
		{
			"dbo.udtValidateCarton",

		};
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}
	public static class RequestAlternativeValidateStoredProcedure
	{
		public static string StoredProcedureName = "requestInsertUpdateDeleteValidateAlternative";
		public static List<string> StoredProcedureParameters = new List<string>()
		{

			"@customerCode",
			"@requestType",
			"@requestNo",
			"@statementType",
			"@requestDetail"

		};
		public static List<string> StoredProcedureTypeNames = new List<string>()
		{
			"dbo.udtValidateAlternativeCarton",

		};
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}

	public static class CustomerPortalRequestApproveStoredProcedure
	{
		public static string StoredProcedureName = "requestCustomerPortalApprove";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@requestNumber",			
			"@processStaus",
			"@userId"
		};
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}

}
