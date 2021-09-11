using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class DailyCollectionStoredProcedure
	{
		public static string StoredProcedureName = "dailyCollectionMarkUpdate";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@requestNo",
			"@mark",
			"@userId"

		};
		
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}
	
}
