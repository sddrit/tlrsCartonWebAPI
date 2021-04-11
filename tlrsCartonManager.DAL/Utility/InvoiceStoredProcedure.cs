using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class InvoiceStoredProcedure
	{
		public static string StoredProcedureName = "invoiceInsertUpdateDelete";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@fromDate",
			"@toDate",
			"@customerId"

		};		
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}
}
