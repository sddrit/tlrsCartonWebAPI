using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class PostingStoredProcedure
	{
		public static string StoredProcedureName = "postingUpdate";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@tackingId",
			"@invisible",
			"@userId"

		};
		
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}
	
}
