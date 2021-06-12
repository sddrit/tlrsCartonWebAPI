using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class GenericReportStoredProcedure
    {
        public static string StoredProcedureName = "reportGenericData";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@menuId",
               "@udtGenericReportFilter"

        };

        public static List<string> StoredProcedureTypeNames = new List<string>()
        {
            "dbo.udtGenericReportFilter",

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) ;
    }
    public class GenericReportColumnStoredProcedure
    {
        public static string StoredProcedureName = "reportGenericColumn";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
               "@menuName"

        };
       
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
    }
}
