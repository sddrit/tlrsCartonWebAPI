using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class DocketBulkStoredProcedure
    {
		public static string StoredProcedureName = "docketInsertUpdateDeleteBulk";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@udtRequestNoList",
			"@printedBy"

		};
		public static List<string> StoredProcedureTypeNames = new List<string>()
		{
			"dbo.udtRequestNoList",

		};
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}
	public static class DocketStoredProcedure
	{
		public static string StoredProcedureName = "docketInsertUpdateDelete";

		public static string StoredProcedureReprintName = "docketRePrint";
		
		public static string StoredProcedureReprintDeleteName = "docketRePrintDelete";

		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@requestNo",
			"@printedBy",
			"@requestType",
			"@serialNo"

		};
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) + " OUTPUT";

		public static string SqlRePrint = "EXEC " + StoredProcedureReprintName + " " + string.Join(",", StoredProcedureParameters);

		public static string SqlRePrintDelete = "EXEC " + StoredProcedureReprintDeleteName + " " + string.Join(",", StoredProcedureParameters);
	}
}
