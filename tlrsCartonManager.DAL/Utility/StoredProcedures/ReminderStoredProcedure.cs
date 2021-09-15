using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class ReminderStoredProcedure
	{
		public static string StoredProcedureName = "requestReminderUpdate";
		public static List<string> StoredProcedureParameters = new List<string>()
		{
			"@requestNo",
			"@reminder1",
			"@reminder2",
			"@reminder3",
			"@userId"

		};
		
		public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
	}
	
}
