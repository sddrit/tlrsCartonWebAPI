using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class UserRoleStoredProcedure
    {
        public static string StoredProcedureName = "userRoleInsertUpdateDelete";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
           "@id",
           "@description",
           "@userId",
           "@statementType",           
           "@udtMenuUserRoleAction"
        };
        public static List<string> StoredProcedureTypeNames = new List<string>()
        {
            
            "dbo.udtMenuUserRoleAction"

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) ;
    }
}
