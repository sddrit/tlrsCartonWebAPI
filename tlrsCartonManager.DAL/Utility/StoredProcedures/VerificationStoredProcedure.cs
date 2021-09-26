using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class VerificationStoredProcedure
	{
		public static string StoredProcedureName = "verificationPickInsertUpdateDelete";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@requestNumber",
			"@cartonNo",
			"@statementType"

		};
		
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}
	
}
