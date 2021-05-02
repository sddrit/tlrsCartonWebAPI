using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class CartonOwnershipStoredProcedure
    {
       
            public static string StoredProcedureName = "ownershipSearchSummary";
            public static List<string> StoredProcedureParameters = new List<string>()
        {
                "@valueFrom",
                "@valueTo",
                "@searchBy"
            };
            public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

        }
    
}
